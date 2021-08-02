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
        List<Vector3> points3d = _lineConverter.ConvertTo3D(points);

        _footEngine.TurnOff();
        
        _bootsBuilder.Clear();
        _drawingPanel.Clear();
        
        yield return new WaitForFixedUpdate();
        
        _playerLift.UpdatePosition(points3d);
        
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate(); // yes, it is necessary

        _bootsBuilder.Build(points3d);

        _footEngine.TurnOn();

    }
}

