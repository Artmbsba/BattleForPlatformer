using System;
using UnityEngine;

public class CollectibleDetector: MonoBehaviour
{
    public event Action<ICollectible> CoinDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectible collectible))
            CoinDetected?.Invoke(collectible);
    }
}