using System;
using System.Collections.Generic;

public interface IReadOnlyReactiveList<T> : IEnumerable<T>
{
    int Count { get; }  

    T this[int index] { get; }

    event Action<T> Added;
    event Action<T> Removed;
    event Action Cleared;

    bool Contains(T item);
}
