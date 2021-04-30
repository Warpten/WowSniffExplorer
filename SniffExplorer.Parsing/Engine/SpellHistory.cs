using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using SniffExplorer.Parsing.Engine.Tracking.Entities;
using SniffExplorer.Parsing.Reactive;
using SniffExplorer.Parsing.Types.ObjectGUIDs;

namespace SniffExplorer.Parsing.Engine
{
    public sealed class SpellHistory : IEnumerable<IObservable<SpellHistory.Entry>>
    {
        public sealed class Entry
        {
            public IObjectGUID Caster     { get; init; }
            public IObjectGUID UnitCaster { get; init; }

            public DateTime SpellStart { get; set; }
            public DateTime SpellGo { get; set; }

            public uint SpellID { get; init; }
            public TimeSpan CastTime => SpellGo - SpellStart;

            public readonly struct MissInfo
            {
                public readonly IObjectGUID Target;
                public readonly uint Reason;
                public readonly uint Extra;
                
                public MissInfo(IObjectGUID target, uint reason, uint extra = 0u)
                {
                    Target = target;
                    Reason = reason;
                    Extra = extra;
                }
            }

            public IObjectGUID? ExplicitTarget { get; set; }
            public IObjectGUID[]? HitTargets { get; set; }
            public MissInfo[]? MissedTargets { get; set; }
        }

        private readonly ConcurrentDictionary<IObjectGUID, EventualObservable<Entry>> _entries = new(comparer: EqualityComparer<IObjectGUID>.Default);

        public IObservable<Entry> this[IObjectGUID castGUID]
            => _entries.GetOrAdd(castGUID, _ => new());

        public IObservable<Entry> Register(IObjectGUID spellCastGUID, IObjectGUID caster, IObjectGUID unitCaster, uint spellID)
        {
            var entry = new Entry {
                Caster = caster,
                UnitCaster = unitCaster,
                SpellID = spellID
            };

            return _entries.AddOrUpdate(spellCastGUID, _ => new EventualObservable<Entry>(entry), (_, value) => {
                value.Value = entry;
                return value;
            });
        }

        public IEnumerator<IObservable<Entry>> GetEnumerator() => _entries.Values.GetEnumerator(); 
        IEnumerator IEnumerable.GetEnumerator() => _entries.Values.GetEnumerator();
    }
}
