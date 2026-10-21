using UnityEngine;

[RequireComponent(typeof(TargetDetector))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _damage = 10f;
    [SerializeField] private float _attackCoolDown = 1.5f;

    private float _lastAttackTime ;

    public bool CanAttack()
    {
        return Time.time - _lastAttackTime > _attackCoolDown ;
    }

    public void HitPlayer(Collider2D playerCollaider)
    {
        if (playerCollaider != null
         && playerCollaider.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);

            _lastAttackTime = Time.time;
        }
    }
}