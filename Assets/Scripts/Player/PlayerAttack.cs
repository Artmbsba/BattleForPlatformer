using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _damage = 20f;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _hitDistance = 1f;

    private bool _canAttack = true;

    public void TryAttack(TargetDetector raycaster, float facingDirection)
    {
        if (_canAttack)
        {
            StartCoroutine(CoolDown());
            HitEnemies(raycaster, facingDirection);
        }
    }

    private void HitEnemies(TargetDetector targetDetector, float facingDirection)
    {
        Collider2D enemyCollaider = targetDetector.FillingAnOpponent(facingDirection, _hitDistance);

        if (enemyCollaider != null
            && enemyCollaider.TryGetComponent<Enemy>(out _)
            && enemyCollaider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);

            Debug.Log("Игрок нанес урон " + _damage);
        }
    }

    private IEnumerator CoolDown()
    {
        var delay = new WaitForSeconds(_attackCooldown);

        _canAttack = false;

        yield return delay;

        _canAttack = true;
    }
}