using UnityEngine;

[RequireComponent (typeof(Health))]
abstract public class Death : MonoBehaviour
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health> ();
    }

    private void OnEnable()
    {
        _health.ÑharacterDied += OnDied;
    }

    private void OnDisable()
    {
        _health.ÑharacterDied -= OnDied;
    }

    abstract protected void OnDied();
}