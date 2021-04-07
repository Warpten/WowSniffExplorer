using System.Collections.Concurrent;
using System.Collections.Generic;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Helpers;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking
{
    public class ObjectManager
    {
        private readonly ConcurrentDictionary<IObjectGUID, IEntity> _entityStore = new();
        private readonly IParseHelper _parseHelper;

        internal ObjectManager(IParseHelper parseHelper) => _parseHelper = parseHelper;

        public IEntity this[IObjectGUID guid]
        {
            get => _entityStore.GetOrAdd(guid, key => _parseHelper.CreateEntity(guid, EntityTypeID.Unknown));
            set => _entityStore.AddOrUpdate(guid, key => value, (key, oldValue) => oldValue);
        }

        public IEntity this[IObjectGUID guid, EntityTypeID entityTypeID]
            => _entityStore.GetOrAdd(guid, key => _parseHelper.CreateEntity(guid, entityTypeID));

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>Do <b>not</b> call this method before parsing is done; results will be unpredictable.</remarks>
        public IEnumerable<IEntity> AsEnumerable() => _entityStore.Values;
        public ICollection<IEntity> AsCollection() => _entityStore.Values;

        public int Count => _entityStore.Count;
    }
}
