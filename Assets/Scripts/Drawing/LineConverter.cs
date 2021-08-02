using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineConverter : MonoBehaviour
{
    [SerializeField] private float _scale;

    public List<Vector3> ConvertTo3D(List<Vector2> points)
    {
        Vector2 pivot = Vector2.negativeInfinity;
        foreach (Vector2 point in points)
        {
            if (point.y > pivot.y)
                pivot = point;
        }
        return points.Select(p => new Vector3(0, (p.y - pivot.y) * _scale,(p.x - pivot.x) * _scale)).ToList();
    }
}