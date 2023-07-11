using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorEnd : MonoBehaviour
{
    [SerializeField] private Transform _respawnPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Fruit>(out var component))
        {
            component.gameObject.transform.position = _respawnPosition.position;
            component.gameObject.SetActive(false);
        }
    }   
}
