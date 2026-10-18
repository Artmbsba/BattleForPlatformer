using System;
using UnityEngine;

public class PatrolZone : SpawnerPoint
{
    [SerializeField] private Enemy _enemyPrefab;

    public event Action<SpawnerPoint, Transform> GiveSpawnCommandEnemy;

    public Enemy EnemyPrefab => _enemyPrefab;

    private void OnEnable()
    {
        _distanceDetection.TargetCameUpForSpawningEnemy += GiveCommandEnemy;
    }

    private void OnDisable()
    {
        _distanceDetection.TargetCameUpForSpawningEnemy -= GiveCommandEnemy;
    }

    private void GiveCommandEnemy(Transform player)
    {
        if (TryMaskSpawned())
            GiveSpawnCommandEnemy?.Invoke(this, player);
    }
}