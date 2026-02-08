using System;
using System.Collections;
using System.Collections.Generic;

public class ReactiveList<T> : IReadOnlyReactiveList<T>
{
    public event Action<T> Added;
    public event Action<T> Removed;
    public event Action Cleared;

    private readonly List<T> _items = new List<T>();

    public int Count => _items.Count;

    public void Add(T item)
    {
        _items.Add(item);
        Added?.Invoke(item);
    }

    public bool Remove(T item)
    {
        if (_items.Remove(item))
        { 
            Removed?.Invoke(item);
            return true;
        }

        return false;
    }

    public void Clear()
    {
        if (_items.Count == 0)
            return;

        _items.Clear();
        Cleared?.Invoke();
    }

    public bool Contains(T item) => _items.Contains(item);

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
