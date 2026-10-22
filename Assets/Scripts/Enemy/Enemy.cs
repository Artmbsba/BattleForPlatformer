using UnityEngine;

[RequireComponent(typeof(TargetDetector))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PatrolRoute))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(EnemyDeath))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animations _animation;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AnimationEvents _animationEvents;
    [SerializeField] private float _attackDistance = 1f;
    [SerializeField] private float _reachDistance = 0.5f;
    [SerializeField] private float _detectionRange = 5f;

    private TargetDetector _targetDetector;
    private PatrolRoute _patrolRoute;
    private Mover _mover;
    private EnemyChase _enemyChase;
    private EnemyDeath _enemyDeath;
    private EnemyAttack _enemyAttack;
    private Health _health;
    private Transform _targetPlayer;

    private float _facingDirection = 1f;
    private bool _isAttacking;

    public float FacingDirection => _facingDirection;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
        _patrolRoute = GetComponent<PatrolRoute>();
        _mover = GetComponent<Mover>();
        _enemyChase = GetComponent<EnemyChase>();
        _enemyDeath = GetComponent<EnemyDeath>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Damaged += OnDamaged;
        _animationEvents.Attacing += OnAttack;
    }

    private void FixedUpdate()
    {
        bool canAttack = _isAttacking == false && _enemyAttack.CanAttack();

        SearchTarget();

        if (ReadyForAnAttack() && canAttack)
            OnAttackAinmation();

        if (_enemyChase.UpdateChase(_detectionRange, _targetPlayer))
            ChasePalyer();
        else
            Patrol();
        _animation.SetSpeed(_mover.Speed);
    }

    private void OnDisable()
    {
        _health.Damaged -= OnDamaged;
        _animationEvents.Attacing -= OnAttack;
    }

    public void SetPatrolZone(PatrolZone patrolZone)
    {
        _patrolRoute.SetPatorlZone(patrolZone);
    }

    private void Patrol()
    {
        Vector2 target = _patrolRoute.CurrentTarget;

        _mover.MoveEnemy(target);

        if (IsTargetReached())
            _patrolRoute.MoveToNext();

        UpdateRotation(target);
    }

    private bool IsTargetReached()
    {
        Vector2 path = _patrolRoute.CurrentTarget - (Vector2)transform.position;

        if (path.sqrMagnitude <= _reachDistance * _reachDistance)
            return true;

        return false;
    }

    private void ChasePalyer()
    {
        Vector2 target = _targetPlayer.position;

        _mover.MoveEnemy(target);

        UpdateRotation(target);
    }

    private bool ReadyForAnAttack()
    {
        if (_targetPlayer != null)
        {
            Vector2 path = (Vector2)_targetPlayer.position - (Vector2)transform.position;

            if (path.sqrMagnitude <= _attackDistance * _attackDistance)
                return true;
        }

        return false;
    }

    private void OnAttackAinmation()
    {
        _animation.TriggerKick();

        _isAttacking = true;
    }

    private void OnAttack()
    {
        if (_targetPlayer != null)
        {
            _enemyAttack.HitPlayer(_targetPlayer);

            _isAttacking = false;
        }
    }

    private void UpdateRotation(Vector2 target)
    {
        float direction = target.x - transform.position.x;

        if (direction != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction);
            transform.localScale = scale;
        }
    }

    private void SearchTarget()
    {
        Collider2D playerCollaider = _targetDetector.FillingAnOpponent(_facingDirection, _detectionRange);

        if (playerCollaider != null && playerCollaider.TryGetComponent(out Player player))
            _targetPlayer = player.transform;
    }

    private void OnDamaged()
    {
        _animation.TriggerHit();
    }
}