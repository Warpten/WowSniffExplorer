using System.Runtime.CompilerServices;

namespace SniffExplorer.Parsing.Types.ObjectGUIDs
{
    public readonly unsafe struct ObjectGuidStream
    {
        private readonly bool[] _flags;

        public delegate ref byte ByteAccessor(int index);

        private readonly ByteAccessor _accessor;

        internal ObjectGuidStream(IObjectGUID guid, int size, ByteAccessor accessor)
        {
            _flags = new bool[size];
            _accessor = accessor;
        }

        public BitRef this[int index] => new(ref _accessor.Invoke(index), ref _flags[index]);

        public readonly ref struct BitRef
        {
            private readonly void* _value;
            private readonly void* _hasValue;

            public BitRef(ref byte value, ref bool hasValue)
            {
                _value = Unsafe.AsPointer(ref value);
                _hasValue = Unsafe.AsPointer(ref hasValue);
            }

            public ref bool HasValue => ref Unsafe.AsRef<bool>(_hasValue);
            public ref byte Value => ref Unsafe.AsRef<byte>(_value);

            public void TryRead(Packet packet)
            {
                if (!HasValue)
                    return;

                ref var value = ref Value;
                value = packet.ReadUInt8();
            }
        }

        public void Initialize(Packet packet, params int[] indices)
        {
            foreach (var indice in indices)
                _flags[indice] = packet.ReadBit();
        }

        public void Parse(Packet packet, params int[] indices)
        {
            foreach (var indice in indices)
            {
                if (_flags[indice])
                {
                    ref var element = ref _accessor.Invoke(indice);
                    element = packet.ReadUInt8();
                }
            }
        }
    }
}