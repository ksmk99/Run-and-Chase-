using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class AnimationController : MonoBehaviour
{
    private Movement movement;
    private Animator animator;

    private void Awake()
    {
        SetComponnents();
    }

    private void LateUpdate()
    {
        animator.SetBool("IsMoving", movement.IsMoving);
    }

    private void SetComponnents()
    {
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }
}