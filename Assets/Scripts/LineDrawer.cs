using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] private Image _circle;
    [SerializeField] private Image _square;

    [SerializeField] private float _width = 10;
    [SerializeField] private Color _color = Color.black;
    
    private List<Image> _stuff = new List<Image>();
    
    public void DrawLine(Vector2 source, Vector2 target)
    {
        Image line = Instantiate(_square, transform);
        line.transform.localPosition = (source + target) / 2;
        line.transform.localRotation = Quaternion.LookRotation(transform.forward, target - source);
        line.transform.localScale = new Vector3(_width, Vector3.Distance(source, target), 1);

        line.color = _color;
        
        _stuff.Add(line);
    }

    public void DrawPoint(Vector2 position)
    {
        Image point = Instantiate(_circle, transform);

        point.transform.localPosition = position;
        point.transform.localScale = new Vector3(_width, _width, 1);

        point.color = _color;
        
        _stuff.Add(point);
    }

    public void Clear()
    {
        foreach (Image gizmo in _stuff)
        {
            Destroy(gizmo);
        }
    }
}