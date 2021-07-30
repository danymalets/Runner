using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _forwardDistance = 20;
    [SerializeField] private float _backDistance = 20;

    [SerializeField] private Ground[] _groundPrefabs;
    
    private LinkedList<Ground> _road = new LinkedList<Ground>();

    private Ground RandomGround => _groundPrefabs[Random.Range(0, _groundPrefabs.Length)];

    private void Start()
    {
        Ground ground = Instantiate(_groundPrefabs[0], transform, false);
        ground.transform.position += transform.position - ground.Start;
        _road.AddLast(ground);
        UpdateHead();
    }
    private void Update()
    {
        UpdateHead();
    }

    private void UpdateHead()
    {
        while (_player.position.z + _forwardDistance > _road.Last.Value.End.z)
        {
            Ground ground = Instantiate(RandomGround, transform, false);
            ground.transform.position += _road.Last.Value.End - ground.Start;
            _road.AddLast(ground);
        }
    }
}