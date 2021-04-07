using System;
using SniffExplorer.Parsing.Helpers.Opcodes;

namespace SniffExplorer.Parsing.Attributes
{
    /// <summary>
    /// Use this attribute on methods that map opcode values to opcodes, depending on client builds.
    /// See <see cref="BaseOpcodeResolver{T}"/> 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class OpcodeResolverAttribute : Attribute
    {
    }
}
