using System;
using System.Collections;
using System.Collections.Generic;
using SniffExplorer.Parsing.Types;

namespace SniffExplorer.Parsing.Engine.Tracking.UpdateFields
{
    public interface IUpdateField
    {
        /// <summary>
        /// The index of the next bit in an <see cref="BitArray">Update mask</see> that does not correspond to this <see cref="IUpdateField"/>.
        /// </summary>
        public int BitEnd { get; }

        public void ReadValue(Packet packet, UpdateMask updateMask);
    }

    public interface IUpdateField<T> : IUpdateField
    {
        public IEnumerable<T> Values { get; }

        public IObservable<(DateTime Moment, T Value)> ValueUpdate { get; }
    }
}
