using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SniffExplorer.Cataclysm.Attributes;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Engine.Tracking.Types;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Cataclysm.Parsing.Handlers
{
    [Flags]
    public enum SplineFlag : uint
    {
        None                = 0x00000000,
        AnimTierSwim        = 0x00000001,
        AnimTierHover       = 0x00000002,
        AnimTierFly         = 0x00000003,
        AnimTierSubmerged   = 0x00000004,
        Unknown0            = 0x00000008,           // NOT VERIFIED
        FallingSlow         = 0x00000010,
        Done                = 0x00000020,
        Falling             = 0x00000040,           // Affects elevation computation, can't be combined with Parabolic flag
        No_Spline           = 0x00000080,
        Unknown2            = 0x00000100,           // NOT VERIFIED
        Flying              = 0x00000200,           // Smooth movement(Catmullrom interpolation mode), flying animation
        OrientationFixed    = 0x00000400,           // Model orientation fixed
        Catmullrom          = 0x00000800,           // Used Catmullrom interpolation mode
        Cyclic              = 0x00001000,           // Movement by cycled spline
        Enter_Cycle         = 0x00002000,           // Everytimes appears with cyclic flag in monster move packet, erases first spline vertex after first cycle done
        Frozen              = 0x00004000,           // Will never arrive
        TransportEnter      = 0x00008000,
        TransportExit       = 0x00010000,
        Unknown3            = 0x00020000,           // NOT VERIFIED
        Unknown4            = 0x00040000,           // NOT VERIFIED
        Backward            = 0x00080000,
        SmoothGroundPath    = 0x00100000,
        CanSwim             = 0x00200000,
        UncompressedPath    = 0x00400000,
        Unknown6            = 0x00800000,           // NOT VERIFIED
        Animation           = 0x01000000,           // Plays animation after some time passed
        Parabolic           = 0x02000000,           // Affects elevation computation, can't be combined with Falling flag
        Final_Point         = 0x04000000,
        Final_Target        = 0x08000000,
        Final_Angle         = 0x10000000,
        Unknown7            = 0x20000000,           // NOT VERIFIED
        Unknown8            = 0x40000000,           // NOT VERIFIED
        Unknown9            = 0x80000000           // NOT VERIFIED
    }

    public class MovementHandler
    {
        [Parser(PacketDirection.ServerToClient, Opcode.SMSG_ON_MONSTER_MOVE)]
        [Parser(PacketDirection.ServerToClient, Opcode.SMSG_ON_MONSTER_MOVE_TRANSPORT)]
        public static void HandleMonsterMove(ParsingContext context, Packet packet)
        {
            var moverGUID = packet.ReadPackedGUID();
            if (context.ObjectManager[moverGUID] is not IUnit entity)
                return;

            var splineInfo = new SplineInfo();

            if (packet.Opcode == Opcode.SMSG_ON_MONSTER_MOVE_TRANSPORT)
            {
                splineInfo.TransportGUID = packet.ReadPackedGUID();
                splineInfo.SeatID = packet.ReadUInt8();
            }

            splineInfo.IsVoluntaryExit = packet.ReadUInt8() != 0;

            // First point ?
            var position = packet.ReadVector3();

            splineInfo.ID = packet.ReadUInt32();
            var splineMode = packet.ReadUInt8();

            switch (splineMode)
            {
                case 3: // Different from the enum... Blizzard, please.
                    splineInfo.Mode = SplineMode.FacingTarget;
                    splineInfo.TargetGUID = packet.ReadGUID();
                    break;
                case 4:
                {
                    splineInfo.Mode = SplineMode.FacingAngle;
                    var orientation = packet.ReadSingle();
                    break;
                }
                case 2:
                    splineInfo.Mode = SplineMode.FacingSpot;
                    splineInfo.FacingSpot = packet.ReadVector3();
                    break;
                case 0:
                    splineInfo.Mode = SplineMode.Normal;
                    break;
                case 1:
                    splineInfo.Mode = SplineMode.Stop;
                    // TODO: Log?
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(splineMode));
            }

            var splineFlags = (SplineFlag) packet.ReadUInt32();
            if (splineFlags.HasFlag(SplineFlag.Animation))
            {
                splineInfo.AnimationTier = packet.ReadUInt8();
                var tierTransitionAnimTime = packet.ReadUInt32();
            }

            splineInfo.Duration = packet.ReadUInt32();

            if (splineFlags.HasFlag(SplineFlag.Parabolic))
            {
                splineInfo.Gravity = packet.ReadSingle();
                var specialTime = packet.ReadUInt32();
            }

            var pointCount = packet.ReadUInt32();
            if (pointCount == 0)
                return;

            if (splineFlags.HasFlag(SplineFlag.UncompressedPath))
            {
                splineInfo.Points = new Vector3[pointCount];
                for (var i = 0; i < splineInfo.Points.Length; ++i)
                    splineInfo.Points[i] = packet.ReadVector3();
            }
            else
            {
                // Blizzard level math
                var middlePosition = (position + packet.ReadVector3()) * 0.5f;

                splineInfo.Points = new Vector3[pointCount - 1];
                for (var i = 0; i < splineInfo.Points.Length; ++i)
                    splineInfo.Points[i] = middlePosition - packet.ReadPackedVector3();
            }

            // TODO: Is this technically still a spline to the target point?
            if (splineInfo.Points.Length == 0)
                return;

            entity.Splines.Insert(packet.Moment, splineInfo);
        }
    }
}
