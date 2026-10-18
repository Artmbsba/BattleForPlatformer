using System;
using UnityEngine;

public class MedKitSpawner : MonoBehaviour
{
    [SerializeField] private MedKitPoint[] _medKitPoints;
    [SerializeField, Range(0f,100f)] private float _spawnChancePercent = 30f;

    private void OnEnable()
    {
        foreach (MedKitPoint medKitPoint in _medKitPoints)
            medKitPoint.GiveSpawnCommand += SpawnMedKit;
    }

    private void OnDisable()
    {
        foreach (MedKitPoint medKitPoint in _medKitPoints)
            medKitPoint.GiveSpawnCommand -= SpawnMedKit;
    }

    private void SpawnMedKit(SpawnerPoint spawnerPoint)
    {
        if (spawnerPoint.TryGetComponent(out MedKitPoint medKitPoint))
            if(SpawnToChance())
            Instantiate(medKitPoint.MedKitPrefab, medKitPoint.transform.position, Quaternion.identity);
    }

    private bool SpawnToChance()
    {
        float minRandomChancePercent = 0f;
        float maxRandomChancePercent = 100f;

        float chancePercent = UnityEngine.Random.Range(minRandomChancePercent, maxRandomChancePercent);

        return chancePercent < _spawnChancePercent;
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _medKitPoints = new MedKitPoint[pointCount];

        for (int i = 0; i < pointCount; i++)
            _medKitPoints[i] = transform.GetChild(i).GetComponent<MedKitPoint>();
    }
#endif
}
