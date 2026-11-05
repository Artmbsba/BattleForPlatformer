using UnityEngine;
using UnityEngine.UI;

public class ViewHPBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private float _speed = 0.5f;

    private Health _health;
    private float _targetValue;

    private void Update()
    {
        _healthBar.value = Mathf.MoveTowards(_healthBar.value, _targetValue, _speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        _health.HealthHasChanged -= DrawHealth;
    }

    public void Setup(Health health)
    {
        _health = health;
        DrawHealth();

        _health.HealthHasChanged += DrawHealth;
    }

    private void DrawHealth()
    {
        _targetValue = _health.CurrentHealth / _health.MaxHealth;
    }
}