using System;
using SniffExplorer.Parsing.Attributes;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Helpers.GUIDs;
using SniffExplorer.Parsing.Helpers.Handlers;
using SniffExplorer.Parsing.Helpers.Opcodes;
using SniffExplorer.Parsing.Helpers.UpdateFields;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Helpers
{
    /// <summary>
    /// Base type of every dispatcher in charge of calling the appropriate method for handling a <see cref="Packet"/>.
    /// This type needs to be inherited from.
    /// </summary>
    /// <typeparam name="T">This type (self-referencing generic).</typeparam>
    /// <typeparam name="U">The type of the attribute declaring which functions handle which opcodes. Needs to supersede <see cref="ParserAttribute"/>.</typeparam>
    public abstract partial class BaseParseHelper<T, U> : IParseHelper
        where T : BaseParseHelper<T, U>
        where U : ParserAttribute
    {
        public ClientBuild ClientBuild => Context.ClientBuild;

        public ParsingContext Context { get; }

        public HandlerHelper Handlers { get; }
        public IOpcodeResolver OpcodeResolver { get; }
        public GuidResolver GuidResolver { get; }
        public UpdateFieldProvider UpdateFieldProvider { get; }

        protected BaseParseHelper(ParsingContext context)
        {
            Context = context;

            var assemblyTypes = typeof(T).Assembly.GetTypes();

            // 1. Load opcode handlers
            Handlers = new HandlerHelper(Context, assemblyTypes);

            // 2. Load opcode resolvers
            var opcodeResolver = default(IOpcodeResolver?);
            foreach (var type in assemblyTypes)
            {
                if (typeof(IOpcodeResolver).IsAssignableFrom(type))
                {
                    if (opcodeResolver != null)
                        throw new InvalidOperationException($"Two (or more) implementations of {nameof(IOpcodeResolver)} were found in {typeof(T).Assembly.FullName}. Fix this!");

                    opcodeResolver = (IOpcodeResolver) Activator.CreateInstance(type)!;
                }
            }

            OpcodeResolver = opcodeResolver ?? throw new InvalidOperationException($"No implementation of {nameof(IOpcodeResolver)} found in {typeof(T).Assembly.FullName}.");
            OpcodeResolver.Initialize();

            // 3. Load GUID resolvers
            GuidResolver = new GuidResolver(Context, assemblyTypes);

            // 4. Load updatefield resolvers
            UpdateFieldProvider = new UpdateFieldProvider(Context, assemblyTypes);
        }

        public abstract EntityTypeID ResolveTypeID(byte value);
        public abstract IEntity CreateEntity(IObjectGUID objectGUID, EntityTypeID typeID);
    }
}