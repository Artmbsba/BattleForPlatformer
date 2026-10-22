using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _damage = 10f;
    [SerializeField] private float _attackCoolDown = 1.5f;

    private float _lastAttackTime ;

    public bool CanAttack()
    {
        return Time.time - _lastAttackTime > _attackCoolDown ;
    }

    public void HitPlayer(Transform player)
    {
        if (player != null
         && player.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);

            _lastAttackTime = Time.time;
        }
    }
}