using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootsUpdater : MonoBehaviour
{
    [SerializeField] private FootEngine _footEngine;
    
    [SerializeField] private DrawingPanel _drawingPanel;
    [SerializeField] private BootsBuilder _bootsBuilder;
    [SerializeField] private LineConverter _lineConverter;
    [SerializeField] private PlayerLift _playerLift;

    private void OnEnable()
    {
        _drawingPanel.Drawn += OnDrawn;
    }
    
    private void OnDisable()
    {
        _drawingPanel.Drawn -= OnDrawn;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(3);
    }

    private void OnDrawn(List<Vector2> points)
    {
        StartCoroutine(UpdateBoots(points));
        
    }

    private IEnumerator UpdateBoots(List<Vector2> points)
    {
        _bootsBuilder.Clear();
        _drawingPanel.Clear();

        _footEngine.TurnOff();

        yield return new WaitForFixedUpdate();
        
        List<Vector3> points3d = _lineConverter.ConvertTo3D(points);
        
        _playerLift.UpdatePosition(points3d);
        
        yield return null;

        _bootsBuilder.Build(points3d);

        _footEngine.TurnOn();

    }
}

