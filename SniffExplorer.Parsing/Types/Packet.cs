using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Types
{
    /// <summary>
    /// A simple wrapper around a <see cref="Stream" /> encapsulating a packet's bytes.
    /// </summary>
    public class Packet
    {
        public readonly DateTime Moment;
        public readonly Opcode Opcode;
        public readonly PacketDirection Direction;

        private readonly ParsingContext _context;
        private readonly Stream _dataStream;
        
#if DEBUG
        private static int _indexGenerator = 0;
        public int Index { get; }

        public long Position => _dataStream.Position;
#endif

        internal Packet(byte[] dataStream, DateTime moment, Opcode opcode, PacketDirection direction, ParsingContext context)
        {
            Moment = moment;
            Opcode = opcode;
            Direction = direction;

            _dataStream = new MemoryStream(dataStream);

            _context = context;
#if DEBUG
            Index = _indexGenerator++;
#endif
        }

        public bool FinalizeRead()
        {
            if (_dataStream.Position == _dataStream.Length)
            {
                _dataStream.Dispose();

                return true;
            }

            _dataStream.Dispose();
            return false;
        }

        private T Read<T>() where T : unmanaged
        {
            Span<byte> data = stackalloc byte[Unsafe.SizeOf<T>()];
            _dataStream.Read(data);
            return MemoryMarshal.Read<T>(data);
        }

        public ulong ReadUInt64() => Read<ulong>();
        public uint ReadUInt32() => Read<uint>();
        public ushort ReadUInt16() => Read<ushort>();
        public byte ReadUInt8() => Read<byte>();

        public long ReadInt64() => Read<long>();
        public int ReadInt32() => Read<int>();
        public short ReadInt16() => Read<short>();
        public sbyte ReadInt8() => Read<sbyte>();

        public float ReadSingle() => Read<float>();

        public IObjectGUID ReadGUID()
        {
            var guid = _context.Helper.GuidResolver.CreateGUID();
            guid.FromPacket(this, false);
            return guid;
        }
        
        public IObjectGUID ReadPackedGUID()
        {
            var guid = _context.Helper.GuidResolver.CreateGUID();
            guid.FromPacket(this, true);
            return guid;
        }

        public Vector3 ReadVector3()
            => new(ReadSingle(), ReadSingle(), ReadSingle());

        public Vector3 ReadPackedVector3()
        {
            int packed = ReadInt32();
            float x = ((packed & 0x7FF) << 21 >> 21) * 0.25f;
            float y = ((((packed >> 11) & 0x7FF) << 21) >> 21) * 0.25f;
            float z = ((packed >> 22 << 22) >> 22) * 0.25f;
            return new(x, y, z);
        }

        /// <summary>
        /// Reads a null-terminated C string from the packet.
        /// </summary>
        /// <param name="encoding">The encoding of the string.</param>
        /// <param name="limit">The maximum length of said string.</param>
        /// <returns></returns>
        public string ReadCString(Encoding encoding, int? limit = default)
        {
            var bytes = new List<byte>();

            if (limit.HasValue)
            {
                byte b;
                while ((b = Read<byte>()) != 0 && limit > 0)
                {
                    bytes.Add(b);
                    --limit;
                }
            }
            else
            {
                byte b;
                while ((b = Read<byte>()) != 0)
                    bytes.Add(b);
            }

            return encoding.GetString(bytes.ToArray());
        }

        /// <see cref="ReadCString(Encoding, int?)"/>
        public string ReadCString(int? limit = default) => ReadCString(Encoding.UTF8, limit);

        public void Skip<T>(int count = 1) where T : unmanaged
        {
            _dataStream.Position += Unsafe.SizeOf<T>() * count;
        }

        /// <summary>
        /// Reads a string for which the size is known (fixed, or previously sent in the packet).
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <returns>The string as read from the packet.</returns>
        public string ReadString(uint length)
        {
            var bytes = ReadBytes(length);
            for (var i = 0; i < length; ++i)
                if (bytes[i] == 0)
                    return Encoding.UTF8.GetString(bytes, 0, i);

            return Encoding.UTF8.GetString(bytes);
        }

        public byte[] ReadBytes(uint length)
        {
            var buffer = new byte[length];
            _dataStream.Read(buffer);
            return buffer;
        }

        private byte _bitpos = 8;
        private byte _curbitval;

        /// <summary>
        /// Reads a bit from the stream. Does not necessarily advance the bit stream.
        /// </summary>
        /// <returns></returns>
        public bool ReadBit()
        {
            if (_bitpos == 8)
            {
                _bitpos = 0;
                _curbitval = Read<byte>();
            }

            var bit = ((_curbitval >> (7 - _bitpos)) & 1) != 0;
            ++_bitpos;
            return bit;
        }

        public void ResetBitReader()
        {
            _bitpos = 8;
            _curbitval = 0;
        }

        public uint ReadBits(int bits)
        {
            uint value = 0;
            for (var i = bits - 1; i >= 0; --i)
                if (ReadBit())
                    value |= (uint)(1 << i);

            return value;
        }

        public ulong ReadPackedUInt64()
        {
            var mask = ReadUInt8();
            if (mask == 0)
                return 0;

            Span<byte> parts = stackalloc byte[8];
            
            for (var i = 0; i < 8; ++i)
                if ((mask & (1 << i)) != 0)
                    parts[i] = ReadUInt8();

            return MemoryMarshal.Read<ulong>(parts);
        }

        public bool CanRead() => _dataStream.Position != _dataStream.Length;
    }
}
