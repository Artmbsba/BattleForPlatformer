using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxValue = 100f;

    private float _current;

    public event Action ÑharacterDied;
    public event Action DamageReceived;
    public event Action HasChanged;

    public float CurrentValue => _current;
    public float MaxValue => _maxValue;
    public bool IsAlive => _current > 0;

    private void Awake()
    {
        _current = _maxValue;
    }

    public void TakeDamage(float damage)
    {
        if (IsAlive && damage < 0)
        return;

            _current = Mathf.Max(0, _current - damage);

            DamageReceived?.Invoke();

            if (_current <= 0)
                ÑharacterDied?.Invoke();

            HasChanged?.Invoke();
    }

    public bool Heal(float healthUnits)
    {
        bool isHealed = false;

        if (healthUnits < 0)
            return isHealed;

            if (_current != _maxValue)
            {
                _current = Mathf.Min(_maxValue, _current + healthUnits);

                HasChanged?.Invoke();

                isHealed = true;
            }
        
        return isHealed;
    }
}