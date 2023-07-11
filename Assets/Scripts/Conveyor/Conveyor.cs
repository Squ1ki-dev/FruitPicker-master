using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _direction;

    private void OnCollisionStay(Collision collision)
    {
        collision.gameObject.transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }
}
