using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPool : MonoBehaviour
{
    [SerializeField] private int _fruitsCount;
    [SerializeField] private bool _autoExpand;

    [SerializeField] private float _spawnRate;

    [field: SerializeField] public FruitsData FruitsData { get; set;}

    private Pool<Fruit> _pool;

    public void PoolStart(Task<Fruit> task)
    {
        _pool = new Pool<Fruit>(FruitsData.FruitTypes, transform, _autoExpand);
        _pool.InitPool(_fruitsCount, task);

        StartCoroutine("SpawnFruitsCoroutine");
    }

    private IEnumerator SpawnFruitsCoroutine()
    {
        while (true)
        { 
            var element = _pool.GetFreeElement();
            element.gameObject.SetActive(true);
            
            yield return new WaitForSecondsRealtime(_spawnRate);
        }
    } 
}
