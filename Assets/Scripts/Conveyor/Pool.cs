using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private T[] _prefabs;
    private bool _autoExpand;
    private Transform _container;
    private Queue<T> _pool;

    public bool HasFreeElement(out T element)
    {
        for (int i = 0; i < _pool.Count; i++)
        {
            T currentElement = _pool.Dequeue();
            _pool.Enqueue(currentElement);

            if (!currentElement.gameObject.activeInHierarchy)
            {
                element = currentElement;
                element.transform.position = _container.position;
                element.transform.SetParent(_container.transform);

                return true;
            }
        }

        element = null;
        return false;
    }

    private T CreateRandObject(bool isActiveByDefault = false)
    {       
        var createdObject = Object.Instantiate(_prefabs[Random.Range(0,_prefabs.Length)], _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Enqueue(createdObject);

        return createdObject;
    }

    private T CreateObject(T prefab, bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Enqueue(createdObject);

        return createdObject;
    }

    public T GetFreeElement()
    {
        if(HasFreeElement(out var element)) return element;

        if (_autoExpand)
            return CreateRandObject(true);

        throw new System.Exception($"There is'nt free element in pool of type {typeof(T)}");
    }

    public Pool(T[] prefabs, Transform container = null, bool autoExpand = false)
    {
        _prefabs = prefabs;
        _container = container;
        _autoExpand = autoExpand;
    }

    public void InitPool(int count, Task<T> task)
    {
        int[] randomIndexes = new int[task.Count];

        for (int i = 0; i < task.Count; i++)
        {
            randomIndexes[i] = Random.Range(0, count);
        }

        _pool = new Queue<T>();

        for (int i = 0; i < count; i++)
        {
            CreateRandObject();

            for (int j = 0; j < randomIndexes.Length; j++)
            {
                if(randomIndexes[j] == i)
                    CreateObject(task.Goal);
            }
        }
    }
}
