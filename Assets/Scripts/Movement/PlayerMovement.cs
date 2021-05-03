using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : Movement
{
	[SerializeField] protected float speed = 20f;
	[SerializeField] private VariableJoystick joystick;

	private float turnSmoothVelocity;
	private Vector3 moveDirection;

	protected override void Move()
	{
		if (joystick.Horizontal != 0 || joystick.Vertical != 0)
		{
			var horizontal = joystick.Horizontal;
			var vertical = joystick.Vertical;

			moveDirection = new Vector3(horizontal, 0, vertical).normalized;
			agent.Move(moveDirection * speed * Time.deltaTime);
			IsMoving = true;
		}
		else IsMoving = false;
	}

	protected override void LookAtTarget()
	{
		if (moveDirection.sqrMagnitude != 0)
		{
			var targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
			var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
				ref turnSmoothVelocity, turnSmoothTime);

			transform.rotation = Quaternion.Euler(0f, angle, 0f);
		}
	}
}
