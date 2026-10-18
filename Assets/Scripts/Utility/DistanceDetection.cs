using System;
using UnityEngine;

public class DistanceDetection : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _minimalDistanceForDetected = 8f;

    private Vector3 _spawnPoint;
    private Vector3 _directionToPlayer;
    private float _distanceToPlayer;

    public event Action TargetCameUpForSpawning;
    public event Action<Transform> TargetCameUpForSpawningEnemy;

    private void Start()
    {
        _spawnPoint = transform.position;
    }

    private void Update()
    {
        _directionToPlayer = _player.transform.position - _spawnPoint;
        _distanceToPlayer = _directionToPlayer.magnitude;

        if (_distanceToPlayer < _minimalDistanceForDetected)
        {
            TargetCameUpForSpawning?.Invoke();
            TargetCameUpForSpawningEnemy?.Invoke(_player.transform);
        }
    }
}