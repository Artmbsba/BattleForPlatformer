using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private CoinPoint[] _coinPoints;

    private void OnEnable()
    {
        foreach (CoinPoint coinPoints in _coinPoints)
            coinPoints.GiveSpawnCommand += SpawnCoin;
    }

    private void OnDisable()
    {
        foreach (CoinPoint coinPoints in _coinPoints)
            coinPoints.GiveSpawnCommand -= SpawnCoin;
    }

    private void SpawnCoin(SpawnerPoint spawnerPoint)
    {
        if (spawnerPoint.TryGetComponent(out CoinPoint coinPoint))
            Instantiate(coinPoint.CoinPrefab, coinPoint.transform.position, Quaternion.identity);
    }

#if UNITY_EDITOR
    [ContextMenu("Refresh Child Array")]
    private void RefreshChildArray()
    {
        int pointCount = transform.childCount;
        _coinPoints = new CoinPoint[pointCount];

        for (int i = 0; i < pointCount; i++)
            _coinPoints[i] = transform.GetChild(i).GetComponent<CoinPoint>();
    }
#endif
}