using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FruitsData", menuName = "Data/FruitsData")]
public class FruitsData : ScriptableObject
{
    [field: SerializeField] public Fruit[] FruitTypes { get; set; }

    public Fruit GetRandElement()
    {
        return FruitTypes[Random.Range(0, FruitTypes.Length)];
    }
}
