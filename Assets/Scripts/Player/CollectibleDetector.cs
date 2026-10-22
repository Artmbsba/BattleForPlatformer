using System;
using UnityEngine;

public class CollectibleDetector: MonoBehaviour
{
    public event Action<ICollectible> CollectibleDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectible collectible))
            CollectibleDetected?.Invoke(collectible);
    }
}