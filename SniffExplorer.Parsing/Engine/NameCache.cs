using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Subjects;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Types.ObjectGUIDs;
using Observable = System.Reactive.Linq.Observable;

namespace SniffExplorer.Parsing.Engine
{
    public class NameCache
    {
        private readonly ConcurrentDictionary<ulong, string> _entityNames = new();
        private readonly ConcurrentDictionary<ulong, string> _creatureNames = new();
        private readonly ConcurrentDictionary<ulong, string> _gameObjectNames = new();

        private int _playerGenerator = 0;

        public void Register(ObjectGuidType guidType, uint entry, string name)
        {
            switch (guidType)
            {
                case ObjectGuidType.Player:
                    _entityNames[entry] = name;
                    break;
                case ObjectGuidType.Creature:
                case ObjectGuidType.Vehicle:
                case ObjectGuidType.Pet:
                    _creatureNames[entry] = name;
                    break;
                case ObjectGuidType.GameObject:
                    _gameObjectNames[entry] = name;
                    break;
            }
        }

        public void Register(IObjectGUID guid, string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            var type = guid.Type;
            Register(type, type == ObjectGuidType.Player ? guid.Low : guid.Entry!.Value, name);
        }

        public string? this[IObjectGUID guid]
        {
            get
            {
                switch (guid.Type)
                {
                    case ObjectGuidType.Player:
                    {
                        return _entityNames.TryGetValue(guid.Low, out var name)
                            ? name
                            : (_entityNames[guid.Low] = $"Player #{_playerGenerator++}");
                    }
                    case ObjectGuidType.Creature:
                    case ObjectGuidType.Vehicle:
                    case ObjectGuidType.Pet:
                    {
                        return _creatureNames.TryGetValue(guid.Entry.GetValueOrDefault(0), out var name)
                            ? name
                            : (_creatureNames[guid.Entry.GetValueOrDefault(0)] = $"Creature #{guid.Entry}");
                    }
                    case ObjectGuidType.GameObject:
                    {
                        return _gameObjectNames.TryGetValue(guid.Entry.GetValueOrDefault(0), out var name)
                            ? name
                            : (_gameObjectNames[guid.Entry.GetValueOrDefault(0)] = $"GameObject #{guid.Entry}");
                    }
                    default:
                        return null;
                }
            }
        }
    }
}