using System;

public class Point : SpawnerPoint
{
    public event Action<SpawnerPoint> GiveSpawnCommand;

    private void OnEnable()
    {
        _distanceDetection.TargetCameUpForSpawning += GiveCommand;
    }

    private void OnDisable()
    {
        _distanceDetection.TargetCameUpForSpawning -= GiveCommand;
    }

    private void GiveCommand()
    {
        if (TryMaskSpawned())
            GiveSpawnCommand?.Invoke(this);
    }
}