using System;
using UnityEngine;

public class CoinDetector : MonoBehaviour
{
    public event Action<Coin> CoinDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
            CoinDetected?.Invoke(coin);
    }
}