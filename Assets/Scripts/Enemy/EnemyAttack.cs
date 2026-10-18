using UnityEngine;

[RequireComponent(typeof(TargetDetector))]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField,Range(0f,100f)] private float _damage = 10f;
    [SerializeField] private float _attackDistance = 0.5f;
    [SerializeField] private float _attackCoolDown = 1.5f;

    private TargetDetector _targetDetector;
    private float _lastAttackTime;
    private float _facingDirection;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
    }

    private void Update()
    {
        if (Time.time - _lastAttackTime > _attackCoolDown)
        {
            Collider2D playerCollaider = _targetDetector.FillingAnOpponent(_facingDirection);

            if (playerCollaider != null
                && playerCollaider.TryGetComponent(out IDamageable damageable)
                && playerCollaider.TryGetComponent<Player>(out _))
            {
                damageable.TakeDamage(_damage);

                _lastAttackTime = Time.time;
                
                Debug.Log("Враг нанес урон " + _damage);
            }
        }
    }

    public void SetFacingDerection(float facingDirection)
    {
        _facingDirection = facingDirection;
    }
}