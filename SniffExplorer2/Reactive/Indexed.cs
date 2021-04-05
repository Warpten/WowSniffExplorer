using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniffExplorer.UI.Reactive
{
    public readonly struct Indexed<T> : IEquatable<Indexed<T>>
    {
        public T Value { get; }
        public int Index { get; }

        public Indexed(T value, int index)
        {
            Value = value;
            Index = index;
        }

        public void Deconstruct(out T value, out int index) => (value, index) = (Value, Index);

        public bool Equals(Indexed<T> other) => other.Index.Equals(Index) && EqualityComparer<T>.Default.Equals(Value, other.Value);

        public static bool operator ==(Indexed<T> first, Indexed<T> second) => first.Equals(second);
        public static bool operator !=(Indexed<T> first, Indexed<T> second) => !first.Equals(second);

        public override bool Equals(object obj) => obj is Indexed<T> o && Equals(o);

        public override int GetHashCode()
        {
            // TODO: Use proper hash code combiner.
            return Index.GetHashCode() ^ (Value?.GetHashCode() ?? 1963);
        }

        public override string ToString() => string.Format(CultureInfo.CurrentCulture, "{0}@{1}", Value, Index);
    }
}
