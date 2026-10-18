using UnityEngine;

public class CoinPoint : Point
{
    [SerializeField] private Coin _coinPrefab;

    public Coin CoinPrefab => _coinPrefab;
}