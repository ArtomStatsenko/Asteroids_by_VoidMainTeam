using System.Collections.Generic;
using UnityEngine;

public sealed class GameObjectPool
{
	private readonly Queue<GameObject> _queue = new Queue<GameObject>();
	private readonly Transform _root;
	private GameObject _prefab;
	private int _index = 0;

	public GameObjectPool(string rootName)
	{
		_root = new GameObject($"[{rootName}]").transform;
	}

	public GameObjectPool(GameObject prefab)
	{
		_prefab = prefab;
		_root = new GameObject($"[{_prefab.name}]").transform;
	}

	public GameObject SetGameObject { set => _prefab = value; }

	public GameObject GetGameObject()
	{
		GameObject _gameObject;
		if (_queue.Count > 0)
		{
			_gameObject = _queue.Dequeue();
		}
		else
		{
			_index++;
			_gameObject = Object.Instantiate(_prefab);
			_gameObject.name = $"{_prefab.name}({_index})";
		}

		_gameObject.SetActive(true);
		_gameObject.transform.SetParent(null);
		return _gameObject;
	}

	public void AddToQueue(GameObject gameObject)
	{
		if (_queue.Contains(gameObject))
		{
			return;
		}

		_queue.Enqueue(gameObject);
		gameObject.transform.SetParent(_root);
		gameObject.SetActive(false);
	}

	public void Dispose()
	{
		for (var i = 0; i < _queue.Count; i++)
		{
			var gameObject = _queue.Dequeue();
			Object.Destroy(gameObject);
		}
		Object.Destroy(_root.gameObject);
	}
}