using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BootsBuilder))]
public class PlayerEngine : MonoBehaviour
{
    [SerializeField] private Rigidbody _root;
    [SerializeField] private HingeJoint _rotator;
    [SerializeField] private DrawingPanel _drawingPanel;
    [SerializeField] private BootsBuilder _bootsBuilder;

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
        _bootsBuilder.Clear();
        _bootsBuilder.Build(points);
        _root.isKinematic = false;
        _rotator.useMotor = true;
    }
}
