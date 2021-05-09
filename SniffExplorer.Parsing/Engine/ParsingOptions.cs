using SniffExplorer.Parsing.Versions;

namespace SniffExplorer.Parsing.Engine
{
    public readonly struct ParsingOptions
    {
        /// <summary>
        /// When this option is set to <see cref="bool">true</see>, descriptors (also know as update fields)
        /// will not keep an history of their value. Only the last value seen in sniffs will be kept.
        ///
        /// You should use this in situations where you need to parse a sniff only for movements, or spells.
        /// </summary>
        public bool DiscardUpdateFields { get; init; }

        /// <summary>
        /// When set to <see cref="bool">true</see>, <b>value update</b> blocks seen in <see cref="Opcode.SMSG_UPDATE_OBJECT"/> will
        /// be discarded if a <b>create</b> block was not received prior.
        /// </summary>
        public bool DiscardUnknownEntities { get; init; }
    }
}