using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _damage = 20f;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _hitDistance = 1f;

    private bool _canAttack = true;

    public void TryAttack(TargetDetector raycaster)
    {
        if (_canAttack)
        {
            StartCoroutine(CoolDown());
            HitEnemies(raycaster);
        }
    }

    private void HitEnemies(TargetDetector targetDetector)
    {
        Enemy enemyTarget = targetDetector.FillingAnOpponent<Enemy>(_hitDistance);

        if (enemyTarget == null)
            return;

        if (enemyTarget != null
         && enemyTarget.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_damage);
    }

    private IEnumerator CoolDown()
    {
        var delay = new WaitForSeconds(_attackCooldown);

        _canAttack = false;

        yield return delay;

        _canAttack = true;
    }
}