using System;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking
{
    public enum SplineMode
    {
        FacingAngle  = 0,
        FacingSpot   = 1,
        FacingTarget = 2,
        Normal       = 3
    }

    public enum SplineType
    {
        Linear     = 0,
        CatmullRom = 1,
        Smooth     = 2 // 4.x
    }

    public class MovementInfo
    {
        public bool PlayHoverAnim { get; set; }
        public bool SuppressedGreetings { get; set; }
        public bool NoBirthAnim { get; set; }
        public bool EnablePortals { get; set; }

        public IObjectGUID? TargetGUID { get; set; }

        public class SplineData
        {
            public SplineMode Mode { get; set; }
            public SplineType Type { get; set; }
            public uint Flags { get; set; }

            public float? VerticalAcceleration { get; set; }

            public float? DurationModNext { get; set; }

            public uint? Duration { get; set; }
            public float? DurationMod { get; set; }

            public uint? TimeElapsed { get; set; }

            public IObjectGUID? FacingTarget { get; set; }
            public float? FacingAngle { get; set; }
            public Vector3? FacingSpot { get; set; }
            
            public uint? StartTime { get; set; }

            public uint ID { get; set; }
            public Vector3 FinalPoint { get; set; }
        }

        public SplineData Spline { get; set; }

        public class VehicleData
        {
            public float Orientation { get; set; }
            public uint ID { get; set; }
        }

        public class LivingData
        {
            public IObjectGUID? GUID { get; set; }
            public uint? MovementFlags { get; set; }
            public uint? MovementFlagsExtra { get; set; }

            public float? RunBackSpeed { get; set; }

            public class FallData
            {
                public DirectionData? Direction { get; set; }
                public uint Time { get; set; }
                public float VerticalSpeed { get; set; }

                public class DirectionData
                {
                    public float HorizontalSpeed { get; set; }
                    public float Sin { get; set; }
                    public float Cos { get; set; }
                }
            }
            
            public FallData? Fall { get; set; }
            
            public float SwimBackSpeed { get; set; }
            public float SwimSpeed { get; set; }
            public float WalkSpeed { get; set; }
            public float FlightSpeed { get; set; }
            public float RunSpeed { get; set; }
            public float FlightBackSpeed { get; set; }

            public float? Pitch { get; set; }

            public float TurnRate { get; set; }
            public float PitchRate { get; set; }

            public float? SplineElevation { get; set; }

            public Vector4 Position { get; } = new Vector4();

            public uint? Time { get; set; }
        }

        public class TransportData
        {
            public IObjectGUID? GUID { get; set; }
            
            public uint[]? StopFrames { get; set; }

            public uint Time { get; set; }
            public uint? PreviousTime { get; set; }

            public Vector4 Offset { get; } = new Vector4();
            public uint? VehicleID { get; set; }

            public byte Seat { get; set; }
        }

        public class GameObjectTransportData
        {
            public IObjectGUID GUID { get; set; }

            public Vector4 Offset { get; } = new Vector4();

            public uint Time { get; set; }
            public uint? PreviousTime { get; set; }

            public byte Seat { get; set; }

            public uint? Vehicle { get; set; }
        }

        public GameObjectTransportData? GameObjectTransport { get; set; }
        public LivingData? Living { get; set; }
        public TransportData? Transport { get; set; }
        public VehicleData? Vehicle { get; set; }

        public Vector4? Stationary { get; set; }

        public uint PathProgress { get; set; }
    }
}