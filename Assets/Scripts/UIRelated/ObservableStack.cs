using System.Collections.Generic;

public delegate void UpdateStackEvent();

class ObservableStack<T> : Stack<T>
{
    public event UpdateStackEvent OnPush;
    public event UpdateStackEvent OnPop;
    public event UpdateStackEvent OnClear;

    public new void Push(T item)
    {
        base.Push(item);

        OnPush?.Invoke();
    }

    public new T Pop()
    {
        T item = base.Pop();

        OnPop?.Invoke();

        return item;
    }

    public new void Clear()
    {
        base.Clear();

        OnClear?.Invoke();
    }
}
