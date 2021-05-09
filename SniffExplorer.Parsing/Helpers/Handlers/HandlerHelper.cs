using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SniffExplorer.Parsing.Attributes;
using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Types;
using SniffExplorer.Parsing.Versions;
using SniffExplorer.Shared.Enums;

namespace SniffExplorer.Parsing.Helpers.Handlers
{
    /// <summary>
    /// This object is in charge of dispatching packet processing to the appropriately declared methods.
    ///
    /// Handlers may have one of the following signatures:
    /// <ul>
    ///     <li>IDisposable[] ProcessPacket(ParsingContext context, Packet packet);</li>
    ///     <li>void ProcessPacket(ParsingContext context, Packet packet);</li>
    /// </ul>
    /// 
    /// Any other return type will cause a runtime exception to occur.
    /// </summary>
    public class HandlerHelper
    {
        // Stores instances of 
        private readonly Dictionary<Type, object> _instances = new();
        private readonly Dictionary<(PacketDirection Direction, Opcode Opcode), Func<ParsingContext, Packet, IDisposable[]>> _dataStore = new();
        
        /// <summary>
        /// Given a <see cref="MethodInfo"/>, constructs a delegate that can be called for packet processing.
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private Func<ParsingContext, Packet, IDisposable[]> CreateDelegate(MethodInfo methodInfo, ParsingContext context)
        {
            var contextParameter = Expression.Parameter(typeof(ParsingContext), "context");
            var packetParameter = Expression.Parameter(typeof(Packet), "packet");
            
            if (methodInfo.IsStatic)
            {
                var methodCallExpr = Expression.Call(methodInfo, contextParameter, packetParameter);
                
                if (methodInfo.ReturnType == typeof(void))
                {
                    var lambdaExpression = Expression.Lambda<Action<ParsingContext, Packet>>(methodCallExpr, contextParameter, packetParameter);
                    var lambda = lambdaExpression.Compile();

                    return (parsingContext, packet) => {
                        lambda.Invoke(parsingContext, packet);
                        return new IDisposable[0];
                    };
                }
                else
                {
                    if (methodInfo.ReturnType != typeof(IDisposable[]))
                        throw new InvalidOperationException($"Method {methodInfo.Name} (declared in {methodInfo.DeclaringType!.FullName}) does not have a proper signature. Packet will be skipped.");

                    var lambdaExpression = Expression.Lambda<Func<ParsingContext, Packet, IDisposable[]>>(methodCallExpr, contextParameter, packetParameter);
                    return lambdaExpression.Compile();
                }
            }
            else
            {
                var declaringType = methodInfo.DeclaringType!;
                // Can't use declaringType for the parameter type due to runtime checks in expressions.
                // (Func<object, ...> would not match the actual type)
                var instanceParameter = Expression.Parameter(typeof(object), "instance");

                // Retrieve the instanciated type.
                if (!_instances.TryGetValue(methodInfo.DeclaringType!, out var instance))
                {
                    // If a constructor taking ParsingContext is found, use it. Else, default construct.
                    var constructorInfo = declaringType.GetConstructor(new [] { typeof(ParsingContext) });
                    instance = constructorInfo != null
                        ? Activator.CreateInstance(declaringType, context) 
                        : Activator.CreateInstance(declaringType);

                    _instances[methodInfo.DeclaringType!] = instance ?? throw new InvalidOperationException($"Unable to create an instance of {declaringType.FullName}.");
                }

                var methodCallExpr = Expression.Call(Expression.Convert(instanceParameter, declaringType), methodInfo, contextParameter, packetParameter);

                if (methodInfo.ReturnType == typeof(void))
                {
                    var lambdaExpression = Expression.Lambda<Action<object, ParsingContext, Packet>>(methodCallExpr, instanceParameter, contextParameter, packetParameter);
                    var lambda = lambdaExpression.Compile();

                    return (parsingContext, packet) =>
                    {
                        lambda.Invoke(instance, parsingContext, packet);
                        return new IDisposable[0];
                    };
                }
                else
                {
                    if (methodInfo.ReturnType != typeof(IDisposable[]))
                        throw new InvalidOperationException($"Method {methodInfo.Name} (declared in {methodInfo.DeclaringType!.FullName}) does not have a proper signature. Packet will be skipped.");

                    var lambdaExpression = Expression.Lambda<Func<object, ParsingContext, Packet, IDisposable[]>>(methodCallExpr, instanceParameter, contextParameter, packetParameter);
                    var lambda = lambdaExpression.Compile();

                    return (parsingContext, packet) => lambda.Invoke(instance, parsingContext, packet);
                }
            }
        }
        
        private ParsingContext Context { get; }

        public HandlerHelper(ParsingContext context, Type[] assemblyTypes)
        {
            Context = context;

            foreach (var type in assemblyTypes)
            {
                foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance))
                {
                    var attributes = methodInfo.GetCustomAttributes<ParserAttribute>().ToList();
                    if (attributes.Count == 0)
                        continue;

                    var @delegate = CreateDelegate(methodInfo, context);
                    foreach (var attribute in attributes)
                        _dataStore.Add((attribute.Direction, attribute.Opcode), @delegate);
                }
            }
        }

        public bool CanProcess(PacketDirection direction, Opcode opcode)
            => _dataStore.ContainsKey((direction, opcode));

        /// <summary>
        /// Processes a packet.
        /// </summary>
        /// <param name="packet">The packet to process.</param>
        /// <returns></returns>
        public bool Process(Packet packet)
        {
            if (_dataStore.TryGetValue((packet.Direction, packet.Opcode), out var @delegate))
            {
                var resources = @delegate.Invoke(Context, packet);
                foreach (var resource in resources)
                    Context.RegisterResource(resource);
            }

            return packet.FinalizeRead();
        }
    }
}
