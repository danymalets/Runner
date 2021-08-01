using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BootsBuilder : MonoBehaviour
{
    [SerializeField] private float _scale = 0.01f;
    [SerializeField] private float _width = 0.1f;
    [SerializeField] private float _thickness = 0.1f;
    
    [SerializeField] private Transform _leftBootRoot;
    [SerializeField] private Transform _rightBootRoot;
    
    [SerializeField] private Color _bootColor;

    private List<GameObject> _stuff = new List<GameObject>();
    
    public void Build(List<Vector2> points)
    {
        Vector2 pivot = Vector2.negativeInfinity;
        foreach (Vector2 point in points)
        {
            if (point.y > pivot.y)
                pivot = point;
        }
        List<Vector3> points3d = points.Select(p => new Vector3(0, (p.y - pivot.y) * _scale,(p.x - pivot.x) * _scale)).ToList();
        
        for (int i = 0; i < points3d.Count - 1; i++)
        {
            Vector3 source = points3d[i];
            Vector3 target = points3d[i + 1];
            
            CreateCube(source, target, _leftBootRoot);
            CreateCube(source, target, _rightBootRoot);
        }
        
        foreach (Vector3 point in points3d)
        {
            CreateCylinder(point, _leftBootRoot);
            CreateCylinder(point, _rightBootRoot);
        }
    }
    
    private void CreateCube(Vector3 source, Vector3 target, Transform parent)
    {
        Transform cube = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        cube.SetParent(parent);
        cube.localPosition = (source + target) / 2;
        cube.localRotation = Quaternion.LookRotation(target - source, Vector3.Scale(target - source, new Vector3(1, 1, -1)));
        cube.localScale = new Vector3(_thickness, _width, Vector3.Distance(source, target));

        cube.GetComponent<Renderer>().material.color = _bootColor;
        
        _stuff.Add(cube.gameObject);
    }
    
    private void CreateCylinder(Vector3 point, Transform parent)
    {
        Transform cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder).transform;
        cylinder.SetParent(parent);
        cylinder.localPosition = point;
        cylinder.localRotation = Quaternion.Euler(0, 0, 90);
        cylinder.localScale = new Vector3(_width, _thickness / 2, _width);
        
        cylinder.GetComponent<Renderer>().material.color = _bootColor;

        _stuff.Add(cylinder.gameObject);
    }

    public void Clear()
    {
        foreach (GameObject gizmo in _stuff)
        {
            Destroy(gizmo);
        }
    }
}