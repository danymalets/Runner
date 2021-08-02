using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerLift : MonoBehaviour
{
    [SerializeField] private Transform _leftBootRoot, _rightBootRoot;
    [SerializeField] private float _width;
    
    [SerializeField] private FootEngine _footEngine;

    [SerializeField] private Transform _playerRoot;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private int _raycastRate = 10;
    [SerializeField] private float _distanceToSky = 100f;

    public void UpdatePosition(List<Vector3> points)
    {
        float minY = points.Min(p => p.y);
        float maxY = points.Max(p => p.y);
        float minZ = points.Min(p => p.z);
        float maxZ = points.Max(p => p.z);

        float rootY = _playerRoot.position.y;
        float rootZ = _playerRoot.position.z;
        
        float lowerFootY = Math.Min(_leftBootRoot.position.y, _rightBootRoot.position.y);
        
        float distanceFromPlayerToGround = _width + (maxY - minY) + (rootY - lowerFootY);

        float skyY = rootY + _distanceToSky;

        float distanceFromSkyToGround = float.PositiveInfinity;
        
        for (int i = 0; i <= _raycastRate; i++)
        {
            float skyZ = Mathf.Lerp(rootZ + minZ, rootZ + maxZ, (float) i / _raycastRate);
            
            if (Physics.Raycast(
                new Vector3(0, skyY, skyZ), 
                Vector3.down, 
                out RaycastHit hit, 
                float.PositiveInfinity,
                _groundMask))
            {
                distanceFromSkyToGround = Mathf.Min(distanceFromSkyToGround, hit.distance);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        _playerRoot.GetComponent<Rigidbody>().MovePosition(new Vector3(
            _playerRoot.position.x,
            skyY - distanceFromSkyToGround + distanceFromPlayerToGround, 
            _playerRoot.position.z));
    }
}