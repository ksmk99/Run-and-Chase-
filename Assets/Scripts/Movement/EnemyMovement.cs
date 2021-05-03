using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class EnemyMovement : Movement
{
	[SerializeField] private PointsController pointsController;

	private FieldOfView fieldOfView;

	private Vector3 target;
	private bool isPlayerLastTarget;
	private float turnSmoothVelocity;

	protected override void Awake()
    {
        base.Awake();
		fieldOfView = GetComponent<FieldOfView>();
    }

    protected override void Move()
	{
		if(fieldOfView.HaveTarget)
        {
			transform.GetComponent<BotMovement>()?.EnemyStartChase(transform);

			target = fieldOfView.Target.position;
			isPlayerLastTarget = true;
		}
		else if(isPlayerLastTarget)
        {
			transform.GetComponent<BotMovement>()?.EnemyEndChase(transform);

			target = pointsController.GetNearestPoint(transform.position);
			isPlayerLastTarget = false;
		}
		else if(target == new Vector3() || Vector3.Distance(target, transform.position) <= agent.stoppingDistance)
        {
			target = pointsController.GetRandomPoint();
        }

		agent.SetDestination(target);
		IsMoving = true;
	}

	protected override void LookAtTarget()
	{
		if (agent.velocity.sqrMagnitude != 0)
		{
			var targetAngle = Mathf.Atan2(agent.velocity.x, agent.velocity.z) * Mathf.Rad2Deg;
			var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
				ref turnSmoothVelocity, turnSmoothTime);

			transform.rotation = Quaternion.Euler(0f, angle, 0f);
		}
	}
}
