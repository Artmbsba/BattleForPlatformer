using UnityEngine;

[RequireComponent(typeof(DistanceDetection))]
public abstract class SpawnerPoint : MonoBehaviour
{
    protected DistanceDetection _distanceDetection;
    private bool _isSpawned = false;

    public bool IsSpawned => _isSpawned;

    private void Awake()
    {
        _distanceDetection = GetComponent<DistanceDetection>();
    }

    protected bool TryMaskSpawned()
    {
        if (_isSpawned)
            return false;

        _isSpawned = true;
        return true;
    }
}