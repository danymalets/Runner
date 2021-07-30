using System;
using UnityEngine;

public class PlayerEngine : MonoBehaviour
{
    [SerializeField] private float _startAngle;
    [SerializeField] private float _animationDuration;

    [SerializeField] private Transform _model;
    [SerializeField] private Transform _leftFoot;
    [SerializeField] private Transform _rightFoot;
    
    [SerializeField]  private float _speed;
    private float _rotationAngle;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        //_speed = 360f / _animationDuration;
    }

    private void FixedUpdate(int a, int b)
    {
        _rotationAngle += _speed * Time.fixedDeltaTime;
        _rigidBody.centerOfMass = Vector3.zero;
        _rigidBody.angularVelocity = new Vector3(_speed, 0, 0);

        _leftFoot.rotation = Quaternion.identity;
        _rightFoot.rotation = Quaternion.identity;
    }

    private void LateUpdate()
    {
        _model.rotation = Quaternion.identity;
        _leftFoot.rotation = Quaternion.identity;
        _rightFoot.rotation = Quaternion.identity;
    }

    private float Func(float x)
    {
        return Mathf.Sin(x * 2 - Mathf.PI / 2) * 0.5f + 0.5f + 0.1f;
    }
}
