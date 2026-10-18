using UnityEngine;

public class MedKitPoint : Point
{
    [SerializeField] private MedKit _medKitPrefab;

    public MedKit MedKitPrefab => _medKitPrefab;
}