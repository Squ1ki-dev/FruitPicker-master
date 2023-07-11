using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemaCamera : MonoBehaviour
{
    private Animator _animator;
    
    private void Start() => _animator = GetComponent<Animator>();
    public void MoveToWonPosition() => _animator.SetBool("hasWon", true);
}
