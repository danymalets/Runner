using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Transform _start, _end;

    public Vector3 Start => _start.position;
    public Vector3 End => _end.position;
}