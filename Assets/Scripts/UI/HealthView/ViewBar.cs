using UnityEngine;

public abstract class ViewBar : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.HasChanged += DrawHealth;
    }

    private void Start()
    {
        DrawHealth();
    }

    private void OnDisable()
    {
        Health.HasChanged -= DrawHealth;
    }

    protected abstract void DrawHealth();
}