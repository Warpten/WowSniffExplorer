using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Microsoft.Data.Sqlite;
using SniffExplorer.Parsing.Attributes;
using SniffExplorer.Parsing.Extensions;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Helpers.Opcodes
{
    /// <inheritdoc cref="IOpcodeResolver"/>
    /// <remarks>
    /// Classes that inherit from this type need to provide at least one <b>public instance</b> method
    /// decorated with <see cref="OpcodeResolverAttribute"/>.
    /// </remarks>
    public class BaseOpcodeResolver<T> : IOpcodeResolver
        where T : BaseOpcodeResolver<T>
    {
        protected class ClientBuildStorage
        {
            private readonly Dictionary<PacketDirection, Dictionary<uint, Opcode>> _storage = new();

            public ClientBuildStorage()
            {
                _storage[PacketDirection.ClientToServer] = new Dictionary<uint, Opcode>();
                _storage[PacketDirection.ServerToClient] = new Dictionary<uint, Opcode>();
            }

            public Opcode Resolve(PacketDirection direction, uint value)
                => _storage[direction].TryGetValue(value, out var opcode) ? opcode : Opcode.None;

            public DirectionHelper BeginUpdate(PacketDirection direction)
            {
                return new DirectionHelper(this, _storage[direction]);
            }

            public ClientBuildStorage Register(PacketDirection direction, Opcode opcode, uint value)
            {
                _storage[direction][value] = opcode;
                return this;
            }

            public class DirectionHelper
            {
                private readonly ClientBuildStorage _self;
                private readonly Dictionary<uint, Opcode> _storage;

                public DirectionHelper(ClientBuildStorage self, Dictionary<uint, Opcode> storage)
                {
                    _self = self;
                    _storage = storage;
                }

                public DirectionHelper Update(Opcode opcode, uint value)
                {
                    _storage[value] = opcode;
                    return this;
                }

                public ClientBuildStorage EndUpdate() => _self;
            }
        }

        private readonly Dictionary<ClientBuild, ClientBuildStorage> _storage = new();

        protected BaseOpcodeResolver()
        {
        }

        public void Initialize()
        {
            foreach (var methodInfo in typeof(T).GetMethods(BindingFlags.Instance | BindingFlags.Public))
                if (methodInfo.IsDefined(typeof(OpcodeResolverAttribute)))
                    methodInfo.Invoke(this, null);
        }

        public Opcode Resolve(ClientBuild build, PacketDirection direction, uint value)
        {
            if (!_storage.TryGetValue(build, out var storage))
                return Opcode.None;

            return storage.Resolve(direction, value);
        }

        protected ClientBuildStorage BeginUpdate(ClientBuild build)
        {
            if (!_storage.TryGetValue(build, out var storage))
                _storage[build] = storage = new ClientBuildStorage();

            return storage;
        }

        protected void LoadDatabase(byte[] resource)
        {
            using (var memoryInstance = resource.AsDatabase())
            {
                memoryInstance.Open();

                if (memoryInstance.State != ConnectionState.Open)
                    throw new InvalidOperationException();

                using var command = new SqliteCommand("SELECT * FROM opcodes", memoryInstance);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var clientBuild = ClientBuild.FromBuild((uint) reader.GetInt32("client_build"));
                    if (clientBuild == null)
                        throw new InvalidOperationException();

                    var direction = reader.GetChar("direction") switch
                    {
                        'C' => PacketDirection.ClientToServer,
                        'S' => PacketDirection.ServerToClient,
                        _   => throw new InvalidOperationException()
                    };

                    if (!Enum.TryParse(typeof(Opcode), reader.GetString("name"), out var opcode))
                        throw new InvalidOperationException();

                    var value = reader.GetInt32("value");

                    // Not known
                    if (value == 0)
                        continue;

                    BeginUpdate(clientBuild).Register(direction, (Opcode) opcode!, (uint) value);
                }
            }
        }
    }
}
