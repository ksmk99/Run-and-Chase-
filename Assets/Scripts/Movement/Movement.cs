using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Movement : MonoBehaviour
{
	public bool IsMoving { get; protected set; }

	[SerializeField] protected float turnSmoothTime = 0.2f;

	protected NavMeshAgent agent;

	protected virtual void Awake()
    {
		SetComponents();
	}

	protected virtual void Update()
    {
        Move();
    }

	protected virtual void LateUpdate()
    {
		LookAtTarget();
    }

	protected abstract void Move();

	protected abstract void LookAtTarget();

	protected virtual void SetComponents()
    {
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
	}
}
