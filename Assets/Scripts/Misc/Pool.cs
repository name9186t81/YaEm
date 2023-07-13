using System;
using System.Collections.Generic;

public class Pool<T>
{
	private readonly Queue<T> _ready;
	private readonly Func<T> _createFunc;

	public event Action<T> OnReturn;

	public Pool(Func<T> createFunc)
	{
		_ready = new Queue<T>();
		_createFunc = createFunc;
	}

	public void ReturnToPool(T item)
	{
		_ready.Enqueue(item);
		OnReturn?.Invoke(item);
	}

	public T Get()
	{
		if(_ready.Count != 0) return _ready.Dequeue();
		return _createFunc();
	}

	public IEnumerable<T> Stored => _ready;
}