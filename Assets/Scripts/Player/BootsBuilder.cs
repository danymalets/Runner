using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BootsBuilder : MonoBehaviour
{
    [SerializeField] private Transform _cubePrefab;
    [SerializeField] private Transform _cylinderPrefab;
    
    [SerializeField] private float _width = 0.1f;
    [SerializeField] private float _thickness = 0.1f;
    
    [SerializeField] private Transform _leftBootRoot;
    [SerializeField] private Transform _rightBootRoot;
    
    private List<GameObject> _stuff = new List<GameObject>();

    public void Build(List<Vector3> points)
    {
        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 source = points[i];
            Vector3 target = points[i + 1];
            
            CreateCube(source, target, _leftBootRoot);
            CreateCube(source, target, _rightBootRoot);
        }
        
        foreach (Vector3 point in points)
        {
            CreateCylinder(point, _leftBootRoot);
            CreateCylinder(point, _rightBootRoot);
        }
    }
    
    private void CreateCube(Vector3 source, Vector3 target, Transform parent)
    {
        Transform cube = Instantiate(_cubePrefab, parent);
        cube.localPosition = (source + target) / 2;
        cube.localRotation = Quaternion.LookRotation(target - source,  Quaternion.Euler(-90, 0, 0) * (target - source));
        cube.localScale = new Vector3(_thickness, _width, Vector3.Distance(source, target));
        
        _stuff.Add(cube.gameObject);
    }
    
    private void CreateCylinder(Vector3 point, Transform parent)
    {
        Transform cylinder = Instantiate(_cylinderPrefab, parent);
        cylinder.SetParent(parent);
        cylinder.localPosition = point;
        cylinder.localScale = new Vector3(_width, _thickness / 2, _width);
        
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