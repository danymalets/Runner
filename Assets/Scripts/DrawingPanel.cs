using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineDrawer))]
public class DrawingPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float _minLineLength = 30;
    
    private LineDrawer _lineDrawer;
    private RectTransform _rectTransform;
    
    private List<Vector2> _points = new List<Vector2>();
    
    public event Action<List<Vector2>> Drawn;
    
    private void Awake()
    {
        _lineDrawer = GetComponent<LineDrawer>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 point = GetPosition(eventData);
        _points.Clear();
        _lineDrawer.DrawPoint(point);
        _points.Add(point);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 point = GetPosition(eventData);
        if (Vector2.Distance(point, _points.Last()) > _minLineLength)
        {
            _lineDrawer.DrawLine(_points.Last(), point);
            _lineDrawer.DrawPoint(point);
            _points.Add(point);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 point = GetPosition(eventData);
        _lineDrawer.DrawLine(_points.Last(), point);
        _lineDrawer.DrawPoint(point);
        _points.Add(point);

        Drawn?.Invoke(_points);
    }
    
    public void Clear()
    {
        _lineDrawer.Clear();
    }
    
    private Vector2 GetPosition(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            null,
            out Vector2 position))
        {
            throw new InvalidOperationException();
        }
        
        float x = Mathf.Clamp(position.x, _rectTransform.rect.min.x, _rectTransform.rect.max.x);
        float y = Mathf.Clamp(position.y, _rectTransform.rect.min.y, _rectTransform.rect.max.y);
        
        return new Vector2(x, y);
    }
}