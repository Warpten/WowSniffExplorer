using System;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.Parsing.Handlers
{
    public class UpdateHandler
    {
        enum UpdateType : uint
        {
            Values = 0,
            CreateObject1 = 1,
            CreateObject2 = 2,
            Destroy = 3
        }

        [Attributes.Parser(PacketDirection.ServerToClient, Opcode.SMSG_UPDATE_OBJECT)]
        public static void HandleUpdateObject(ParsingContext context, Packet packet)
        {
            var map = packet.ReadUInt16();
            var updateCount = packet.ReadUInt32();

            for (var i = 0; i < updateCount; ++i)
            {
                // Debug.WriteLine($"[{packet.Index}[{i}] {packet.Position}");

                var updateType = ((UpdateType) packet.ReadUInt8());
                switch (updateType)
                {
                    case UpdateType.Values:
                    {
                        var guid = packet.ReadPackedGUID();
                        HandleValuesUpdate(context, packet, guid, map);
                        break;
                    }
                    case UpdateType.CreateObject1:
                    case UpdateType.CreateObject2:
                    {
                        var guid = packet.ReadPackedGUID();
                        HandleCreateObject(context, packet, guid, map, updateType);
                        break;
                    }
                    case UpdateType.Destroy:
                        HandleDestroy(context, packet, map, updateType);
                        break;
                }
            }
        }

        private static void HandleValuesUpdate(ParsingContext context, Packet packet, IObjectGUID guid, ushort map)
        {
            HandleValuesUpdate(context, packet, context.ObjectManager[guid], map);
        }

        private static void HandleValuesUpdate(ParsingContext context, Packet packet, IEntity entity, ushort map)
        {
            var maskSize = packet.ReadUInt8();
            var updateMask = new uint[maskSize];
            for (var i = 0; i < maskSize; ++i)
                updateMask[i] = packet.ReadUInt32();

            var mask = new UpdateMask(updateMask);
            if (entity == null)
                throw new InvalidOperationException();

            entity.ProcessValuesUpdate(packet, mask);
        }

        private static void HandleDestroy(ParsingContext context, Packet packet, ushort map, UpdateType updateType)
        {
            var objectCount = packet.ReadUInt32();
            for (var i = 0; i < objectCount; ++i)
            {
                var entityGuid = packet.ReadPackedGUID();
                // context.ObjectManager[entityGuid].Destroyed = packet.Moment; // Do we bother?
            }
        }

        private static void HandleCreateObject(ParsingContext context, Packet packet, IObjectGUID guid, ushort map, UpdateType updateType)
        {
            var objectType = context.Helper.ResolveTypeID(packet.ReadUInt8());
            var entity = context.ObjectManager[guid, objectType];

            HandleMovementUpdate(context, packet, entity, map);
            HandleValuesUpdate(context, packet, entity, map);
        }

        private static void HandleMovementUpdate(ParsingContext context, Packet packet, IEntity entity, ushort map)
        {
            var movementSnapshot = entity.MovementInfo[packet.Moment];

            movementSnapshot.PlayHoverAnim = packet.ReadBit();
            movementSnapshot.SuppressedGreetings = packet.ReadBit();
            var hasRotation = packet.ReadBit();
            var hasAnimKits = packet.ReadBit();
            var hasTargetGUID = packet.ReadBit();
            entity.IsSelf = packet.ReadBit();
            var hasVehicleData = packet.ReadBit();
            var hasLivingData = packet.ReadBit();
            var stopFrameCount = packet.ReadBits(24);
            movementSnapshot.NoBirthAnim = packet.ReadBit();
            var hasGoTransportPosition = packet.ReadBit();
            var hasStationaryPosition = packet.ReadBit();
            var hasAreaTrigger = packet.ReadBit();
            movementSnapshot.EnablePortals = packet.ReadBit();
            var hasTransport = packet.ReadBit();

            // Readers
            var livingReader = new LivingReader();
            var goTransportReader = new GoTransportReader();
            var animKitReader = new AnimKitsReader();

            if (hasLivingData)
                livingReader.FirstPass(movementSnapshot, packet, context);

            if (hasGoTransportPosition)
                goTransportReader.FirstPass(movementSnapshot, packet, context);

            if (hasTargetGUID) // 446
            {
                movementSnapshot.TargetGUID = context.Helper.GuidResolver.CreateGUID();
                movementSnapshot.TargetGUID!.AsBitStream().Initialize(packet, 2, 7, 0, 4, 5, 6, 1, 3);
            }

            if (hasAnimKits)
                animKitReader.FirstPass(movementSnapshot, packet, context);

            packet.ResetBitReader();

            // Exit out early if stop frames received for a non-transport
            if (stopFrameCount != 0 && movementSnapshot.Transport == null)
                throw new InvalidOperationException();

            // Shove them in trnasport even though they aren't in the data block itself.
            if (movementSnapshot.Transport == null)
            {
                for (var i = 0; i < stopFrameCount; ++i)
                    packet.ReadUInt32();
            }
            else
            {
                movementSnapshot.Transport!.StopFrames = new uint[stopFrameCount];
                for (var i = 0; i < stopFrameCount; ++i)
                    movementSnapshot.Transport!.StopFrames[i] = packet.ReadUInt32();
            }

            if (hasLivingData)
                livingReader.SecondPass(movementSnapshot, packet, context);

            if (hasVehicleData)
            {
                movementSnapshot.Vehicle = new MovementInfo.VehicleData();

                movementSnapshot.Vehicle.Orientation = packet.ReadSingle();
                movementSnapshot.Vehicle.ID = packet.ReadUInt32();
            }

            if (hasGoTransportPosition)
                goTransportReader.SecondPass(movementSnapshot, packet, context);

            if (hasRotation)
            {
                // Packed Quaternion
                // TODO
                var packedRotation = packet.ReadUInt64();
            }

            if (hasAreaTrigger)
            {
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadUInt8();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
                packet.ReadSingle();
            }

            if (hasStationaryPosition)
            {
                movementSnapshot.Stationary = new Vector4()
                {
                    O = packet.ReadSingle(),
                    X = packet.ReadSingle(),
                    Y = packet.ReadSingle(),
                    Z = packet.ReadSingle()
                };
            }

            if (hasTargetGUID)
                movementSnapshot.TargetGUID!.AsBitStream().Parse(packet, 4, 0, 3, 5, 7, 6, 2, 1);

            if (hasAnimKits)
                animKitReader.SecondPass(movementSnapshot, packet, context);

            if (hasTransport)
                movementSnapshot.PathProgress = packet.ReadUInt32();
        }
    }

    class LivingReader
    {
        private bool _hasOrientation;
        private bool _hasPlayerSplineData;
        private bool _hasSplineElevation;
        private bool _hasPitch;
        private bool _hasSplineData;
        private bool _hasFallData;
        private bool _hasTransportData;
        private bool _hasTime;

        private TransportReader? _transportReader;
        private SplineReader? _splineReader;
        private FallDataReader? _fallDataReader;

        public TransportReader? TransportReader => _transportReader;

        private bool _heightChangeFailed;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Object.cpp:363 to 428
        /// </remarks>
        /// <param name="movementInfo"></param>
        /// <param name="packet"></param>
        /// <param name="context"></param>
        public void FirstPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            movementInfo.Living = new MovementInfo.LivingData();

            var hasMovementFlags = !packet.ReadBit();
            _hasOrientation = !packet.ReadBit();

            movementInfo.Living.GUID = context.Helper.GuidResolver.CreateGUID();
            movementInfo.Living.GUID!.AsBitStream().Initialize(packet, 7, 3, 2);

            if (hasMovementFlags)
                movementInfo.Living.MovementFlags = packet.ReadBits(30);

            _hasPlayerSplineData = packet.ReadBit();
            _hasPitch = !packet.ReadBit();
            _hasSplineData = packet.ReadBit();
            _hasFallData = packet.ReadBit();
            _hasSplineElevation = !packet.ReadBit();
            movementInfo.Living.GUID.AsBitStream()[5].HasValue = packet.ReadBit();
            _hasTransportData = packet.ReadBit();
            _hasTime = !packet.ReadBit();

            if (_hasTransportData)
            {
                _transportReader = new TransportReader();
                _transportReader.FirstPass(movementInfo, packet, context);
            }

            movementInfo.Living.GUID.AsBitStream()[4].HasValue = packet.ReadBit();

            if (_hasSplineData)
            {
                _splineReader = new SplineReader();
                _splineReader.FirstPass(movementInfo, packet, context);
            }

            movementInfo.Living.GUID.AsBitStream()[6].HasValue = packet.ReadBit();

            if (_hasFallData)
            {
                _fallDataReader = new FallDataReader();
                _fallDataReader.FirstPass(movementInfo, packet, context);
            }

            movementInfo.Living.GUID.AsBitStream().Initialize(packet, 0, 1);

            _heightChangeFailed = packet.ReadBit();

            var hasMovementFlagsExtra = !packet.ReadBit();
            if (hasMovementFlagsExtra)
                movementInfo.Living.MovementFlagsExtra = packet.ReadBits(12);
        }

        public void SecondPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            movementInfo.Living!.GUID!.AsBitStream().Parse(packet, 4);
            movementInfo.Living.RunBackSpeed = packet.ReadSingle();

            if (_hasFallData)
                _fallDataReader!.SecondPass(movementInfo, packet, context);

            movementInfo.Living.SwimBackSpeed = packet.ReadSingle();
            if (_hasSplineElevation)
                movementInfo.Living.SplineElevation = packet.ReadSingle();

            if (_hasSplineData)
                _splineReader!.SecondPass(movementInfo, packet, context);

            movementInfo.Living.Position.Z = packet.ReadSingle();
            movementInfo.Living.GUID.AsBitStream().Parse(packet, 5);

            if (_hasTransportData)
                _transportReader!.SecondPass(movementInfo, packet, context);

            movementInfo.Living.Position.X = packet.ReadSingle();
            movementInfo.Living.PitchRate = packet.ReadSingle();
            movementInfo.Living!.GUID!.AsBitStream().Parse(packet, 3, 0);
            movementInfo.Living.SwimSpeed = packet.ReadSingle();
            movementInfo.Living.Position.Y = packet.ReadSingle();
            movementInfo.Living!.GUID!.AsBitStream().Parse(packet, 7, 1, 2);
            movementInfo.Living.WalkSpeed = packet.ReadSingle();
            
            if (_hasTime)
                movementInfo.Living.Time = packet.ReadUInt32();

            movementInfo.Living.FlightBackSpeed = packet.ReadSingle();
            movementInfo.Living!.GUID!.AsBitStream().Parse(packet, 6);
            movementInfo.Living.TurnRate = packet.ReadSingle();

            if (_hasOrientation)
                movementInfo.Living.Position.O = packet.ReadSingle();

            movementInfo.Living.RunSpeed = packet.ReadSingle();
            if (_hasPitch)
                movementInfo.Living.Pitch = packet.ReadSingle();

            movementInfo.Living.FlightSpeed = packet.ReadSingle();
        }
    }

    class SplineReader
    {
        private bool _notFinalized;
        private bool _hasStartTime;
        private bool _hasVerticalAcceleration;

        private uint _pathSize;

        /// <summary>
        /// MovementPacketBuilder.cpp:36
        /// </summary>
        /// <param name="movementInfo"></param>
        /// <param name="packet"></param>
        /// <param name="context"></param>
        public void FirstPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            movementInfo.Spline = new MovementInfo.SplineData();

            _notFinalized = packet.ReadBit();
            if (_notFinalized)
            {
                movementInfo.Spline.Type = (SplineType) packet.ReadBits(2);

                _hasStartTime = packet.ReadBit();
                _pathSize = packet.ReadBits(22);

                movementInfo.Spline.Mode = (SplineMode) packet.ReadBits(2);
                if (movementInfo.Spline.Mode == SplineMode.FacingTarget)
                {
                    movementInfo.Spline.FacingTarget = context.Helper.GuidResolver.CreateGUID();
                    movementInfo.Spline.FacingTarget.AsBitStream().Initialize(packet, 4, 3, 7, 2, 6, 1, 0, 5);
                }

                _hasVerticalAcceleration = packet.ReadBit();
                movementInfo.Spline.Flags = packet.ReadBits(25);
            }
        }

        internal void SecondPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            if (_notFinalized)
            {
                if (_hasVerticalAcceleration)
                    movementInfo.Spline.VerticalAcceleration = packet.ReadSingle();

                movementInfo.Spline.TimeElapsed = packet.ReadUInt32();
                switch (movementInfo.Spline.Mode)
                {
                    case SplineMode.FacingAngle:
                        movementInfo.Spline.FacingAngle = packet.ReadSingle();
                        break;
                    case SplineMode.FacingTarget:
                        movementInfo.Spline.FacingTarget!.AsBitStream().Parse(packet, 5, 3, 7, 1, 6, 4, 2, 0);
                        break;
                    default:
                        break;
                }

                for (var i = 0; i < _pathSize; ++i)
                {
                    var waypoint = new Vector3();
                    waypoint.Z = packet.ReadSingle();
                    waypoint.X = packet.ReadSingle();
                    waypoint.Y = packet.ReadSingle();
                }

                if (movementInfo.Spline.Mode == SplineMode.FacingSpot)
                    movementInfo.Spline.FacingSpot = new Vector3
                    {
                        X = packet.ReadSingle(),
                        Z = packet.ReadSingle(),
                        Y = packet.ReadSingle()
                    };

                movementInfo.Spline.DurationModNext = packet.ReadSingle();
                movementInfo.Spline.Duration = packet.ReadUInt32();

                if (_hasStartTime)
                    movementInfo.Spline.StartTime = packet.ReadUInt32();

                movementInfo.Spline.DurationMod = packet.ReadSingle();
            }

            movementInfo.Spline.FinalPoint = new Vector3
            {
                Z = packet.ReadSingle(),
                X = packet.ReadSingle(),
                Y = packet.ReadSingle()
            };

            movementInfo.Spline.ID = packet.ReadUInt32();
        }
    }

    class FallDataReader
    {
        private bool _hasFallDirection;

        public void FirstPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            movementInfo.Living!.Fall = new MovementInfo.LivingData.FallData();
            
            _hasFallDirection = packet.ReadBit();
        }

        public void SecondPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            if (_hasFallDirection)
            {
                movementInfo.Living!.Fall!.Direction = new MovementInfo.LivingData.FallData.DirectionData();

                movementInfo.Living.Fall!.Direction.HorizontalSpeed = packet.ReadSingle();
                movementInfo.Living.Fall.Direction.Sin = packet.ReadSingle();
                movementInfo.Living.Fall.Direction.Cos = packet.ReadSingle();
            }

            movementInfo.Living!.Fall!.Time = packet.ReadUInt32();
            movementInfo.Living.Fall.VerticalSpeed = packet.ReadSingle();
        }
    }

    class GoTransportReader
    {
        private bool _hasVehicleID;
        private bool _hasPreviousTransportTime;

        public void FirstPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            movementInfo.GameObjectTransport = new MovementInfo.GameObjectTransportData();

            movementInfo.GameObjectTransport.GUID = context.Helper.GuidResolver.CreateGUID();
            movementInfo.GameObjectTransport.GUID.AsBitStream().Initialize(packet, 5);
            _hasVehicleID = packet.ReadBit();
            movementInfo.GameObjectTransport.GUID.AsBitStream().Initialize(packet, 0, 3, 6, 1, 4, 2);
            _hasPreviousTransportTime = packet.ReadBit();
            movementInfo.GameObjectTransport.GUID.AsBitStream().Initialize(packet, 7);
        }

        public void SecondPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            movementInfo.GameObjectTransport!.GUID!.AsBitStream().Parse(packet, 0, 5);
            if (_hasVehicleID)
                movementInfo.GameObjectTransport.Vehicle = packet.ReadUInt32();

            movementInfo.GameObjectTransport!.GUID!.AsBitStream().Parse(packet, 3);
            movementInfo.GameObjectTransport.Offset.X = packet.ReadSingle();
            movementInfo.GameObjectTransport!.GUID!.AsBitStream().Parse(packet, 4, 6, 1);
            movementInfo.GameObjectTransport.Time = packet.ReadUInt32();
            movementInfo.GameObjectTransport.Offset.Y = packet.ReadSingle();
            movementInfo.GameObjectTransport!.GUID!.AsBitStream().Parse(packet, 2, 7);
            movementInfo.GameObjectTransport.Offset.Z = packet.ReadSingle();
            movementInfo.GameObjectTransport.Seat = packet.ReadUInt8();
            movementInfo.GameObjectTransport.Offset.O = packet.ReadSingle();

            if (_hasPreviousTransportTime)
                movementInfo.GameObjectTransport.PreviousTime = packet.ReadUInt32();
        }
    }

    class TransportReader
    {
        private bool _hasPreviousMoveTime;
        private bool _hasVehicleID;

        public void FirstPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            movementInfo.Transport = new MovementInfo.TransportData();

            movementInfo.Transport.GUID = context.Helper.GuidResolver.CreateGUID();
            movementInfo.Transport.GUID.AsBitStream().Initialize(packet, 1);
            _hasPreviousMoveTime = packet.ReadBit();
            movementInfo.Transport.GUID.AsBitStream().Initialize(packet, 4, 0, 6);
            _hasVehicleID = packet.ReadBit();
            movementInfo.Transport.GUID.AsBitStream().Initialize(packet, 7, 5, 3, 2);
        }

        public void SecondPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            movementInfo.Transport!.GUID!.AsBitStream().Parse(packet, 5, 7);
            movementInfo.Transport.Time = packet.ReadUInt32();
            movementInfo.Transport.Offset.O = packet.ReadSingle();
            if (_hasPreviousMoveTime)
                movementInfo.Transport.PreviousTime = packet.ReadUInt32();

            movementInfo.Transport.Offset.Y = packet.ReadSingle();
            movementInfo.Transport.Offset.X = packet.ReadSingle();
            movementInfo.Transport!.GUID!.AsBitStream().Parse(packet, 3);
            movementInfo.Transport.Offset.Z = packet.ReadSingle();
            movementInfo.Transport!.GUID!.AsBitStream().Parse(packet, 0);

            if (_hasVehicleID)
                movementInfo.Transport.VehicleID = packet.ReadUInt32();

            movementInfo.Transport.Seat = packet.ReadUInt8();

            movementInfo.Transport!.GUID!.AsBitStream().Parse(packet, 1, 6, 2, 4);
        }
    }

    class AnimKitsReader
    {
        private bool _hasAiAnimKit;
        private bool _hasMovementAnimKit;
        private bool _hasMeleeAnimKit;

        public void FirstPass(MovementInfo movementInfo, Packet packet, ParsingContext context)
        {
            _hasAiAnimKit = !packet.ReadBit();
            _hasMovementAnimKit = !packet.ReadBit();
            _hasMeleeAnimKit = !packet.ReadBit();
        }

        public void SecondPass(MovementInfo movementSnapshot, Packet packet, ParsingContext context)
        {
            if (_hasAiAnimKit)
                packet.ReadUInt16();

            if (_hasMovementAnimKit)
                packet.ReadUInt16();

            if (_hasMeleeAnimKit)
                packet.ReadUInt16();
        }
    }
}
