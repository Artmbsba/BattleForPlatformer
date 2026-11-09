using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TargetDetector))]
public class VampirismDamager : MonoBehaviour
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _attackCooldown = 0.5f;
    [SerializeField] private float _rangeOfAction = 1f;

    private TargetDetector _targetDetector;
    private bool _canAttack = true;

    public float RangeOfAction => _rangeOfAction;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
    }

    public float Tick()
    {
        float damageDone = 0f;

        if (_canAttack == false)
            return damageDone;

        StartCoroutine(CoolDown());

        damageDone = Hit();

      return damageDone;
    }

    private IEnumerator CoolDown()
    {
        var delay = new WaitForSeconds(_attackCooldown);

        _canAttack = false;

        yield return delay;

        _canAttack = true;
    }

    public Enemy SearchTarget(float radius)
    {
        return _targetDetector.FindClosest<Enemy>(radius);
    }

    private float Hit( )
    {
        Enemy enemyTarget = SearchTarget(_rangeOfAction);

        if (enemyTarget == null)
            return 0f;

        enemyTarget = enemyTarget.GetComponentInParent<Enemy>();

        if (enemyTarget != null
         && enemyTarget.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_damage);

        return _damage;
    }
}