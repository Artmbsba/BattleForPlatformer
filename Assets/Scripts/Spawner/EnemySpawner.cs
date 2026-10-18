using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private PatrolZone[] _patrolZones;

    private void OnEnable()
    {
        foreach (PatrolZone patrolZone in _patrolZones)
            patrolZone.GiveSpawnCommandEnemy += SpawnEnemy;
    }

    private void OnDisable()
    {
        foreach (PatrolZone patrolZone in _patrolZones)
            patrolZone.GiveSpawnCommandEnemy -= SpawnEnemy;
    }

    private void SpawnEnemy(SpawnerPoint spawnerPoint, Transform player)
    {
        if (spawnerPoint.TryGetComponent(out PatrolZone patrolZone))
        {
            Enemy newEnemy = Instantiate(patrolZone.EnemyPrefab, patrolZone.transform.position, Quaternion.identity);

            newEnemy.SetPatrolZone(patrolZone);
            newEnemy.SetTargetPlayer(player);
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _patrolZones = new PatrolZone[pointCount];

        for (int i = 0; i < pointCount; i++)
            _patrolZones[i] = transform.GetChild(i).GetComponent<PatrolZone>();
    }
#endif
}