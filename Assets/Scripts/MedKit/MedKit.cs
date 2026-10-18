using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField,Range (1f, 100f)] private float _healthUnits = 25f;

    public float HealthUnits => _healthUnits;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}