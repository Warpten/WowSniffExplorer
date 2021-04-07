using System.Collections.Generic;

namespace SniffExplorer.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<(T Value, int Index)> Indexed<T>(this IEnumerable<T> sequence)
        {
            var index = 0;
            foreach (var element in sequence)
                yield return (element, index++);
        }
    }
}
