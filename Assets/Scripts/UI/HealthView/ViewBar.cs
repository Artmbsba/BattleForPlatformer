using UnityEngine;

public abstract class ViewBar : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.HealthHasChanged += DrawHealth;
    }

    private void Start()
    {
        DrawHealth();
    }

    private void OnDisable()
    {
        Health.HealthHasChanged -= DrawHealth;
    }

    protected abstract void DrawHealth();
}