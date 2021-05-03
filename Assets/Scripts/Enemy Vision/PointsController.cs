using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    private Transform[] points;

    public Vector3 GetRandomPoint(Transform currentPoint)
    {
        var availiblePoints = points
            .Where(point => point.position != currentPoint.position)
            .ToArray();
        return availiblePoints[Random.Range(0, availiblePoints.Length)].position;
    }

    public Vector3 GetRandomPoint()
    {
        return points[Random.Range(0, points.Length)].position;
    }

    public Vector3 GetFurthestPoint(Vector3 position)
    {
        var minDistance = float.MinValue;
        var result = new Vector3();

        foreach (var point in points)
        {
            var distance = Vector3.Distance(point.position, position);
            if (distance > minDistance)
            {
                minDistance = distance;
                result = point.position;
            }
        }

        return result;
    }

    public Vector3 GetNearestPoint(Vector3 position)
    {
        var minDistance = float.MaxValue;
        var result = new Vector3();

        foreach (var point in points)
        {
            var distance = Vector3.Distance(point.position, position);
            if (distance < minDistance)
            {
                minDistance = distance;
                result = point.position;
            }    
        }

        return result;
    }

    private void Awake()
    {
        SetPoints();
    }

    private void SetPoints()
    {
        points = GetComponentsInChildren<Transform>();
        if (points.Length == 0)
        {
            throw new System.Exception("No targets added");
        }
    }
}
