using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : Movement
{
	[SerializeField] private PointsController pointsController;

	private float turnSmoothVelocity;
	private Vector3 target;

	private List<Transform> enemies = new List<Transform>();

	public void EnemyStartChase(Transform enemy)
    {
		if (!enemies.Contains(enemy))
			enemies.Add(enemy);
    }

	public void EnemyEndChase(Transform enemy)
	{
		if (!enemies.Contains(enemy))
			throw new Exception("Removing a non-existent enemy");
		enemies.Remove(enemy);
	}

	protected override void Move()
	{
		if (enemies.Count > 0)
		{
			target = pointsController.GetFurthestPoint(AvaragePosition());
		}
		else if (target == new Vector3() || Vector3.Distance(target, transform.position) <= agent.stoppingDistance)
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

	private Vector3 AvaragePosition()
    {
		var result = new Vector3();
        foreach (var enemy in enemies)
        {
			result += enemy.position;
        }

		return result / enemies.Count; 
    }
}
