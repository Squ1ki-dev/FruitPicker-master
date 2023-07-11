using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitKind 
{
    Apple,
    Avocado,
    Bannana,
    Cherries,
    Lemon,
    Peach,
    Peanut,
    Pear,
    Strawberry,
    Watermelon
}

public class Fruit : MonoBehaviour
{ 
    [field: SerializeField] public FruitKind Kind { get; set; }

    public void Freeze()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    public void Unfreeze()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}