using System;
using System.Linq.Expressions;
using System.Reflection;

using SniffExplorer.Parsing.Engine;
using SniffExplorer.Parsing.Engine.Tracking.UpdateFields;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using SniffExplorer.Shared.Attributes.Descriptors;

namespace SniffExplorer.Parsing.Helpers.UpdateFields
{
    public class UpdateFieldProvider
    {
        private readonly Func<ParsingContext, IObjectData>? _objectDataBuilder;
        private readonly Func<ParsingContext, IItemData>? _itemDataBuilder;
        private readonly Func<ParsingContext, IContainerData>? _containerDataBuilder;
        private readonly Func<ParsingContext, IDynamicObjectData>? _dynamicObjectDataBuilder;
        private readonly Func<ParsingContext, IGameObjectData>? _gameObjectDataBuilder;
        private readonly Func<ParsingContext, IUnitData>? _unitDataBuilder;
        private readonly Func<ParsingContext, IPlayerData>? _playerDataBuilder;
        private readonly Func<ParsingContext, IActivePlayerData>? _activePlayerDataBuilder;
        private readonly Func<ParsingContext, ICorpseData>? _corpseDataBuilder;
        private readonly Func<ParsingContext, IAreaTriggerData>? _areaTriggerDataBuilder;

        private readonly ParsingContext _context;

        public UpdateFieldProvider(ParsingContext context, Type[] assemblyTypes)
        {
            _context = context;

            foreach (var assemblyType in assemblyTypes)
            {
                var descriptorAttribute = assemblyType.GetCustomAttribute<GeneratedDescriptorAttribute>();
                if (descriptorAttribute == null)
                    continue;

                // Skip if the expansion type does not match, or the build does not.
                if (context.ClientBuild.ExpansionType != descriptorAttribute.RealmType
                    || context.ClientBuild.Value != descriptorAttribute.ClientBuild)
                    continue;

                TryBindLambda(ref _objectDataBuilder, assemblyType);
                TryBindLambda(ref _itemDataBuilder, assemblyType);
                TryBindLambda(ref _containerDataBuilder, assemblyType);
                TryBindLambda(ref _unitDataBuilder, assemblyType);
                TryBindLambda(ref _playerDataBuilder, assemblyType);
                TryBindLambda(ref _activePlayerDataBuilder, assemblyType);
                TryBindLambda(ref _areaTriggerDataBuilder, assemblyType);
                TryBindLambda(ref _dynamicObjectDataBuilder, assemblyType);
                TryBindLambda(ref _gameObjectDataBuilder, assemblyType);
                TryBindLambda(ref _corpseDataBuilder, assemblyType);
            }
        }

        private void TryBindLambda<T>(ref Func<ParsingContext, T>? fn, Type actualType)
        {
            if (typeof(T).IsAssignableFrom(actualType))
            {
                var contextParameter = Expression.Parameter(typeof(ParsingContext));

                // Search for the constructor
                var constructor = actualType.GetConstructor(new[] { typeof(ParsingContext) });
                if (constructor == null)
                    throw new InvalidOperationException($"Constructor not found on {actualType.FullName}. Code generation likely failed.");

                var instanceExpr = Expression.New(constructor, contextParameter);
                fn = Expression.Lambda<Func<ParsingContext, T>>(instanceExpr, contextParameter).Compile();
            }
        }

        public IObjectData? CreateObjectData(IObjectGUID guid)
            => _objectDataBuilder?.Invoke(_context);

        public IItemData? CreateItemData(IObjectGUID guid)
            => _itemDataBuilder?.Invoke(_context);

        public IContainerData? CreateContainerData(IObjectGUID guid)
            => _containerDataBuilder?.Invoke(_context);

        public IUnitData? CreateUnitData(IObjectGUID guid)
            => _unitDataBuilder?.Invoke(_context);

        public IPlayerData? CreatePlayerData(IObjectGUID guid)
            => _playerDataBuilder?.Invoke(_context);

        public IActivePlayerData? CreateActivePlayerData(IObjectGUID guid)
            => _activePlayerDataBuilder?.Invoke(_context);

        public IAreaTriggerData? CreateAreaTriggerData(IObjectGUID guid)
            => _areaTriggerDataBuilder?.Invoke(_context);

        public IDynamicObjectData? CreateDynamicObjectData(IObjectGUID guid)
            => _dynamicObjectDataBuilder?.Invoke(_context);

        public IGameObjectData? CreateGameObjectData(IObjectGUID guid)
            => _gameObjectDataBuilder?.Invoke(_context);

        public ICorpseData? CreateCorpseData(IObjectGUID guid)
            => _corpseDataBuilder?.Invoke(_context);
    }
}
