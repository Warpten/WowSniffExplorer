using System;
using System.Diagnostics;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SniffExplorer.Parsing.Reactive
{
    /// <summary>
    /// An implementation of a single-value subject that stores its first emitted value, and only emits one value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventualObservable<T> : ObservableBase<T>
    {
        private readonly BehaviorSubject<T?> _subject;

        public EventualObservable(T? value = default)
        {
            _subject = new BehaviorSubject<T?>(value);
        }
        
        protected override IDisposable SubscribeCore(IObserver<T> observer)
        {
            if (_subject.Value != null)
            {
                observer.OnNext(_subject.Value!);

                return Disposable.Empty;
            }
            
            // Keep taking values until a non-null value is received and only emit that one.
            return _subject.TakeUntil(value => value != null).LastAsync().Subscribe(value => {
                Debug.Assert(value != null, "value != null");

                observer.OnNext(value!);
            }, observer.OnError, observer.OnCompleted);
        }
        
        public T Value
        {
            get => _subject.Value!;
            set => _subject.OnNext(value);
        }

        public bool HasValue => _subject.TryGetValue(out var value) && value != null;
    }
}
