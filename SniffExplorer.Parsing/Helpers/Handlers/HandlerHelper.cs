using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using SniffExplorer.Parsing.Attributes;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Helpers.Handlers
{
    /// <summary>
    /// This interface is in charge of dispatching packet processing to the appropriate methods.
    /// </summary>
    public class HandlerHelper
    {
        private class Store
        {
            private readonly Dictionary<Type, object> _instances = new();
            private readonly Dictionary<(PacketDirection Direction, Opcode Opcode), Action<ParsingContext, Packet>> _dataStore = new();

            internal void Insert(ParsingContext context, PacketDirection direction, Opcode value, MethodInfo methodInfo)
            {
                var contextParameter = Expression.Parameter(typeof(ParsingContext), "context");
                var packetParameter = Expression.Parameter(typeof(Packet), "packet");

                if (methodInfo.IsStatic)
                {
                    var methodCallExpr = Expression.Call(methodInfo, contextParameter, packetParameter);
                    var lambda = Expression
                        .Lambda<Action<ParsingContext, Packet>>(methodCallExpr,
                            contextParameter, packetParameter);

                    Insert(direction, value, lambda.Compile());
                }
                else
                {
                    var declaringType = methodInfo.DeclaringType!;
                    // Can't use declaringType for the parameter type due to runtime checks in expressions.
                    var instanceParameter = Expression.Parameter(typeof(object), "instance");

                    var methodCallExpr =
                        Expression.Call(Expression.Convert(instanceParameter, declaringType), methodInfo, contextParameter, packetParameter);
                    var lambda = Expression.Lambda<Action<object, ParsingContext, Packet>>(methodCallExpr, instanceParameter, contextParameter, packetParameter);

                    var compiledLambda = lambda.Compile();

                    if (!_instances.TryGetValue(methodInfo.DeclaringType!, out var instance))
                    {
                        // If a constructor taking ParsingContext is found, use it. Else, default construct.
                        var constructorInfo = declaringType.GetConstructor(new [] { typeof(ParsingContext) });
                        instance = constructorInfo != null
                            ? Activator.CreateInstance(declaringType, context) 
                            : Activator.CreateInstance(declaringType);

                        _instances[methodInfo.DeclaringType!] = instance
                            ?? throw new InvalidOperationException($"Unable to create an instance of {declaringType.FullName}.");
                    }

                    Insert(direction, value, (ctx, packet) => compiledLambda.Invoke(instance!, ctx, packet));
                }
            }

            public bool CanProcess(PacketDirection direction, Opcode opcode)
                => _dataStore.ContainsKey((direction, opcode));

            private void Insert(PacketDirection direction, Opcode opcode, Action<ParsingContext, Packet> fn)
                => _dataStore[(direction, opcode)] = fn;

            public void InvokeMethod(ParsingContext context, Packet packet)
            {
                if (_dataStore.TryGetValue((packet.Direction, packet.Opcode), out var @delegate))
                    @delegate.Invoke(context, packet);
            }

            public async Task InvokeMethodAsync(ParsingContext context, Packet packet)
                => await Task.Run(() => InvokeMethod(context, packet)).ConfigureAwait(false);
        }

        private readonly Store _packetHandlers = new();

        private ParsingContext Context { get; }

        public HandlerHelper(ParsingContext context, Type[] assemblyTypes)
        {
            Context = context;

            foreach (var type in assemblyTypes)
            {
                foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                {
                    var attributes = methodInfo.GetCustomAttributes<ParserAttribute>();
                    foreach (var attribute in attributes)
                        _packetHandlers.Insert(context, attribute.Direction, attribute.Opcode, methodInfo);
                }
            }
        }

        public bool CanProcess(PacketDirection direction, Opcode opcode)
            => _packetHandlers.CanProcess(direction, opcode);

        /// <summary>
        /// Processes a packet.
        /// </summary>
        /// <param name="packet">The packet to process.</param>
        /// <returns></returns>
        public bool Process(Packet packet)
        {
            _packetHandlers.InvokeMethod(Context, packet);

            return packet.FinalizeRead();
        }

        /// <summary>
        /// Asynchronously processes a packet.
        /// </summary>
        /// <param name="packet">The packet to process.</param>
        /// <returns></returns>
        public async Task<bool> ProcessAsync(Packet packet)
        {
            await _packetHandlers.InvokeMethodAsync(Context, packet);

            return packet.FinalizeRead();
        }
    }
}
