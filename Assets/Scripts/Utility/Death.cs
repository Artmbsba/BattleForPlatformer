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
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    abstract protected void OnDied();
}