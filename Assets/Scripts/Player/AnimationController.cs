using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Transform _rotator;
    [SerializeField] private Animator _animator;

    [Range(0, 360)]
    [SerializeField] private float _offset;
    
    private void Update()
    {
        float angle = (_rotator.localRotation.eulerAngles.x + _offset) % 360;
        _animator.Play(PlayerAnimator.Animations.Walking, -1, angle / 360);
    }
}