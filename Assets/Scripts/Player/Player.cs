using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _touchRadius;

    [SerializeField] private float _pickUpSpeed;
    [SerializeField] private float _fruitFlySpeed;

    [SerializeField] private Basket _basket;

    [SerializeField] private GameObject _hand,
                                        _handController;

    private TaskManager _taskManager;
    public Fruit TargetFruit { get; set; }
    private Animator _animator;
    private AudioSource _audioSource;
    private Camera _mainCamera;

    private void Start() => Init();

    private void Init()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _taskManager = GetComponent<TaskManager>();

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        ChangeTarget();
        HandControl();
    }

    private void HandControl()
    {
        if(TargetFruit == null) return;

        _handController.transform.position = Vector3.Lerp(_handController.transform.position, TargetFruit.transform.position, Time.deltaTime * _pickUpSpeed);
        TargetFruit.Freeze();
        TargetFruit.transform.position = Vector3.Lerp(TargetFruit.transform.position, _hand.transform.position, Time.deltaTime * _fruitFlySpeed);
    }

    private void ChangeTarget()
    {
        if (TargetFruit == null)
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.SphereCast(ray, _touchRadius, out hit))
            {
                var targetObject = hit.transform.gameObject;               

                if (targetObject != null && targetObject.TryGetComponent<Fruit>(out var targetFruit))
                {
                    TargetFruit = targetFruit;
                    _animator.SetTrigger("isPickingUp");
                    _audioSource.Play();
                }
            }
        }
    }

    public void Won() => _animator.SetBool("hasWon",true);

    private void Drop()
    {
        _basket.Set(TargetFruit);
        _taskManager.PutFruit(TargetFruit);

        TargetFruit = null;
    }
}
