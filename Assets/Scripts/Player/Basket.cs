using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] private Slot _slot;

    public void Set(Fruit fruit)
    {
        if(fruit == null) return;
        
        if (_slot.Fruit != null)
        {
            _slot.Fruit.Unfreeze();
            _slot.Fruit.gameObject.SetActive(false);
            _slot.transform.DetachChildren();

            _slot.Fruit = fruit;
        }
        else
            _slot.Fruit = fruit;

        fruit.transform.SetParent(_slot.transform);
        fruit.transform.localPosition = Vector3.zero;
    }
} 
  

