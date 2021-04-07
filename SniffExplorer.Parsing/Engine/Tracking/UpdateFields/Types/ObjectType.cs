using System;
using MemoryMarshal = System.Runtime.InteropServices.MemoryMarshal;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields.Types
{
    public class ObjectType
    {
        public ushort TypeMask { get; }
        public bool IsInGuild { get; }

        private ObjectType(ushort type, bool isInGuild)
        {
            TypeMask = type;
            IsInGuild = isInGuild;
        }

        public static ObjectType FromRawData(Span<uint> values)
        {
            var wordSpan = MemoryMarshal.Cast<uint, ushort>(values);

            return new ObjectType(wordSpan[0], wordSpan[1] != 0);
        }
    }
}
