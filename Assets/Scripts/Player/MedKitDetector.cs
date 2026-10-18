using System;
using UnityEngine;

public class MedKitDetector: MonoBehaviour
{
    public event Action<MedKit> MedKitDetected;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out MedKit medKit))
            MedKitDetected?.Invoke(medKit);
    }
}