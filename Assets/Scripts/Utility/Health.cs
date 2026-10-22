using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    public event Action Died;
    public event Action Damaged;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public bool IsAlive => _currentHealth > 0;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (IsAlive && damage >= 0)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - damage);

            Damaged?.Invoke();

            if (_currentHealth <= 0)
                Died?.Invoke();
        }
    }

    public bool Heal(float healthUnits)
    {
        bool isHealed = false;

        if (healthUnits >= 0)
        {
            if (_currentHealth != _maxHealth)
            {
                _currentHealth = Mathf.Min(_maxHealth, _currentHealth + healthUnits);

                isHealed = true;
            }
            else
            {
                Debug.Log("Лечение не требуется!");
            }
        }

        return isHealed;
    }
}