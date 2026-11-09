using System.Collections;
using UnityEngine;

[RequireComponent(typeof(VampirismDamager))]
public class Vampirism : MonoBehaviour
{
    [SerializeField] private VampirismView _vampirismView;
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldownDuration = 4f;

    private VampirismDamager _vampirismDamager;
    private Health _health;

    private bool _isActive = false;

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

        _vampirismView.StartAbility(_vampirismDamager.RangeOfAction);

        while (elapsedTime < _abilityDuration)
        {
            float healAmount = _vampirismDamager.Tick();

            if (healAmount > 0f)
                HealPlayer(healAmount);

            _vampirismView.Tick(elapsedTime, _abilityDuration);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _vampirismView.StartCooldown();

        float elapsedCooldown = 0f;

        while (elapsedCooldown < _cooldownDuration)
        {
            float remainingTime = _cooldownDuration - elapsedCooldown;
            _vampirismView.Tick(remainingTime, _cooldownDuration);

            elapsedCooldown += Time.deltaTime;

            yield return null;
        }

        _isActive = false;
    }
}