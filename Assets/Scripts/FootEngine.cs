using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEngine : MonoBehaviour
{
    [SerializeField] private Rigidbody _root;
    [SerializeField] private HingeJoint _rotator;
    
    [SerializeField] private float _targetVelocity = 200f;
    
    public void TurnOn()
    {
        _root.isKinematic = false;
        SetTargetVelocity(_targetVelocity);
    }

    public void TurnOff()
    {
        _root.isKinematic = true;
        SetTargetVelocity(0);
    }

    private void SetTargetVelocity(float targetVelocity)
    {
        JointMotor rotatorMotor = _rotator.motor;
        rotatorMotor.targetVelocity = targetVelocity;
        _rotator.motor = rotatorMotor;
    }
}