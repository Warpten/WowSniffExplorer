using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace SniffExplorer.Shared.Extensions
{
    public static class ObservableExtensions
    {
        public static IObservable<(T Value, int Index)> Indexed<T>(this IObservable<T> source)
            => source.Select((elem, index) => (elem, index));
    }
}
