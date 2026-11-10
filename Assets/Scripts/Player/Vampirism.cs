using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VampirismDamager))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;

    private VampirismDamager _vampirismDamager;
    private Health _health;

    private bool _isActive = false;

    public event Action<float> AbilityStarted;
    public event Action<float, float> Ticked;
    public event Action CooldownStarted;

    private void Awake()
    {
        _vampirismDamager = GetComponent<VampirismDamager>();
    }

    public void TryVampirism(Health health)
    {
        if (_isActive)
            return;

        _health = health;

        StartCoroutine(StartAbility());
    }

    private void HealPlayer(float healUnit)
    {
        _health.Heal(healUnit);
    }

    private IEnumerator StartAbility()
    {
        _isActive = true;

        float elapsedTime = 0f;

        AbilityStarted?.Invoke(_vampirismDamager.RangeOfAction);

        while (elapsedTime < _abilityDuration)
        {
            float healAmount = _vampirismDamager.Tick();

            if (healAmount > 0f)
                HealPlayer(healAmount);

            Ticked?.Invoke(elapsedTime, _abilityDuration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        CooldownStarted?.Invoke();

        float elapsedCooldown = 0f;

        while (elapsedCooldown < _cooldownDuration)
        {
            float remainingTime = _cooldownDuration - elapsedCooldown;
            Ticked?.Invoke(remainingTime, _cooldownDuration);

            elapsedCooldown += Time.deltaTime;

            yield return null;
        }

        _isActive = false;
    }
}