using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Misc;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Loading.Implementations
{
    public class PktSniffLoader : ISniffFileLoader
    {
        private readonly ushort _pktVersion;

        private readonly DateTime _startTime;
        private readonly uint _startTickCount;

        public ClientBuild ClientBuild { get; }
        public byte[] SessionKey { get; }

        public int Count { get; set; }

        public PktSniffLoader(Stream dataStream)
        {
            using var reader = new BinaryReader(dataStream, Encoding.ASCII, true);

            var header = reader.ReadBytes(3);
            if (Encoding.ASCII.GetString(header) != "PKT")
                throw new InvalidOperationException("Bad file header");

            _pktVersion = reader.ReadUInt16();

            switch (_pktVersion)
            {
                case 0x0201: // 2.1
                {
                    var clientBuild = reader.ReadUInt16();
                    ClientBuild = ClientBuild.FromBuild(clientBuild) ??
                                  throw new InvalidOperationException($"Client build {clientBuild} is not supported");

                    SessionKey = reader.ReadBytes(40);
                    break;
                }
                case 0x0202: // 2.2
                {
                    var snifferId = reader.ReadByte();

                    var clientBuild = reader.ReadUInt32();
                    ClientBuild = ClientBuild.FromBuild(clientBuild) ??
                                  throw new InvalidOperationException($"Client build {clientBuild} is not supported");

                    var locale = reader.ReadUInt32();
                    SessionKey = reader.ReadBytes(40);
                    reader.BaseStream.Position += 64; // Realm name
                    break;
                }
                case 0x0300: // 3.0
                {
                    var snifferId = reader.ReadByte();

                    var clientBuild = reader.ReadUInt32();
                    ClientBuild = ClientBuild.FromBuild(clientBuild) ??
                                  throw new InvalidOperationException($"Client build {clientBuild} is not supported");

                    var locale = reader.ReadUInt32();
                    SessionKey = reader.ReadBytes(40);
                    var additionalLength = reader.ReadUInt32();

                    if (snifferId == 10) // Xyla
                    {
                        _startTime = Utilities.GetDateTimeFromUnixTime(reader.ReadUInt32());
                        _startTickCount = reader.ReadUInt32();
                        reader.BaseStream.Position += additionalLength - 4 - 4;
                    }
                    else
                        reader.BaseStream.Position += additionalLength;

                    break;
                }
                case 0x0301: // 3.1
                {
                    var snifferId = reader.ReadByte();

                    var clientBuild = reader.ReadUInt32();
                    ClientBuild = ClientBuild.FromBuild(clientBuild) ??
                                  throw new InvalidOperationException($"Client build {clientBuild} is not supported");

                    var locale = reader.ReadUInt32();
                    SessionKey = reader.ReadBytes(40);

                    _startTime = Utilities.GetDateTimeFromUnixTime(reader.ReadUInt32());
                    _startTickCount = reader.ReadUInt32();

                    var additionalLength = reader.ReadInt32();
                    var optionalData = reader.ReadBytes(additionalLength);
                    if (snifferId == 'S') // WSTC
                    {
                        var snifferVersion = 0x0105;
                        // Versions 1.5 and older store human readable sniffer description string in header
                        // Version 1.6 adds 3 bytes before that data, 0xFF separator, one byte for major version and one byte for minor version, expecting 0x0106 for 1.6
                        if (additionalLength >= 3 && optionalData[0] == 0xFF)
                            snifferVersion = BitConverter.ToInt16(optionalData, 1);

                        if (snifferVersion >= 0x0107)
                            _startTime = DateTime.FromFileTimeUtc(BitConverter.ToInt64(optionalData, 3));
                    }

                    break;
                }
                default:
                    throw new InvalidOperationException("This version of the PKT file format is not handled.");
            }
        }

        public IEnumerable<Packet> Parse(Stream dataStream, ParsingContext context)
        {
            var count = 0;
            while (dataStream.Position != dataStream.Length)
            {
                ++count;
                var packet = ParsePacketCore(dataStream, context);
                if (packet != null)
                    yield return packet;
            }

            Count = count;
        }

        private Packet? ParsePacketCore(Stream dataStream, ParsingContext context)
        {
            if (dataStream.Position == dataStream.Length)
                return null;

            using var reader = new BinaryReader(dataStream, Encoding.ASCII, true);

            switch (_pktVersion)
            {
                case 0x0201: // 2.1
                case 0x0202: // 2.2
                {
                    var direction = reader.ReadByte() == 0xFF
                        ? PacketDirection.ServerToClient
                        : PacketDirection.ClientToServer;
                    var packetTime = Utilities.GetDateTimeFromUnixTime(reader.ReadUInt32());
                    // Unfortunately unusable because we don't know tick count at sniff start.
                    // GetTickCount() returns the number of milliseconds since system start.
                    var tickCount = reader.ReadUInt32();

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
                    var length = direction switch
                    {
                        PacketDirection.ServerToClient => reader.ReadInt32() - Unsafe.SizeOf<ushort>(),
                        PacketDirection.ClientToServer => reader.ReadInt32() - Unsafe.SizeOf<uint>(),
                    };

                    var opcode = direction switch
                    {
                        PacketDirection.ServerToClient => context.Helper.OpcodeResolver.Resolve(context.ClientBuild, direction, reader.ReadUInt16()),
                        PacketDirection.ClientToServer => context.Helper.OpcodeResolver.Resolve(context.ClientBuild, direction, reader.ReadUInt32())
                    };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).

                    if (context.Helper.Handlers.CanProcess(direction, opcode))
                        return context.CreatePacket(reader.ReadBytes(length), packetTime, opcode, 0u, direction);
                    
                    reader.BaseStream.Position += length;
                    return null;
                }
                case 0x0300: // 3.0
                case 0x0301: // 3.1
                {
                    var direction = reader.ReadUInt32() switch
                    {
                        0x47534D53 => PacketDirection.ServerToClient,
                        0x47534D43 => PacketDirection.ClientToServer,
                        0x4E425F53 => PacketDirection.BNServerToClient,
                        _ => PacketDirection.BNClientToServer,
                    };

                    var connectionIndex = 0u;
                    DateTime packetTime;
                    if (_pktVersion == 0x0300)
                    {
                        packetTime = Utilities.GetDateTimeFromUnixTime(reader.ReadUInt32());
                        var tickCount = reader.ReadUInt32();
                        if (_startTickCount != 0)
                            packetTime = _startTime.AddMilliseconds(tickCount - _startTickCount);
                    }
                    else
                    {
                        connectionIndex = reader.ReadUInt32();
                        var tickCount = reader.ReadUInt32();
                        packetTime = _startTime.AddMilliseconds(tickCount - _startTickCount);
                        packetTime = DateTime.SpecifyKind(packetTime, DateTimeKind.Utc);
                        packetTime = TimeZoneInfo.ConvertTimeFromUtc(packetTime, TimeZoneInfo.Local);
                    }

                    var additionalSize = reader.ReadUInt32();
                    var length = reader.ReadInt32() - 4;
                    reader.BaseStream.Position += additionalSize;

                    var opcode = context.Helper.OpcodeResolver.Resolve(context.ClientBuild, direction, reader.ReadUInt32());
                    if (context.Helper.Handlers.CanProcess(direction, opcode))
                        return context.CreatePacket(reader.ReadBytes(length), packetTime, opcode, connectionIndex, direction);
                    
                    reader.BaseStream.Position += length;
                    return null;
                }
                default:
                    return null;
            }
        }
    }
}
