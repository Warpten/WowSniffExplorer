using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Helpers;
using SniffExplorer.Parsing.Reactive;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine.Tracking
{
    public class ObjectManager
    {
        private readonly ConcurrentDictionary<IObjectGUID, IEntity> _entityStore = new();
        private readonly IParseHelper _parseHelper;
        
        internal ObjectManager(IParseHelper parseHelper) => _parseHelper = parseHelper;

        // TODO: Again, bad situation to be in: we don't know who the local player is (we **could** guess it from any CMSG opcode...)
        public IEntity this[IObjectGUID guid]
             => _entityStore.GetOrAdd(guid, key => _parseHelper.CreateEntity(key, EntityTypeID.Unknown, true));

        public IEntity this[IObjectGUID guid, EntityTypeID entityTypeID, bool isSelf]
            =>  _entityStore.GetOrAdd(guid, key => _parseHelper.CreateEntity(key, entityTypeID, isSelf));

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
