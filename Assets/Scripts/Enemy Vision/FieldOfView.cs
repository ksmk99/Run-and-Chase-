using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public bool HaveTarget { get; private set; }
	public Transform Target { get => visibleTarget; }

	public float viewRadius;
	[Range(0, 360)] public float viewAngle;

	[SerializeField] private LayerMask targetMask;
	[SerializeField] private LayerMask obstacleMask;

	private Transform visibleTarget;

	private void Update()
	{
		FindVisibleTargets();
	}

	private void FindVisibleTargets()
	{
		var targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
		HaveTarget = false;
        foreach (var targetInViewRadius in targetsInViewRadius)
		{
			var target = targetInViewRadius.transform;
			var direction = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, direction) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, direction, dstToTarget, obstacleMask))
				{
					visibleTarget = target;
					HaveTarget = true;
				}
			}
		}
	}
}
