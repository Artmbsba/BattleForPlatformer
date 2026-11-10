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
    private EnemyChase _chase;
    private EnemyDeath _death;
    private EnemyAttack _attack;
    private Health _health;
    private Transform _targetPlayer;

    private bool _isAttacking;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
        _patrolRoute = GetComponent<PatrolRoute>();
        _mover = GetComponent<Mover>();
        _chase = GetComponent<EnemyChase>();
        _death = GetComponent<EnemyDeath>();
        _attack = GetComponent<EnemyAttack>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.DamageReceived += OnDamaged;
        _animationEvents.Attacing += OnAttack;
    }

    private void FixedUpdate()
    {
        bool canAttack = _isAttacking == false && _attack.CanAttack();

        SearchTarget();

        if (ReadyForAnAttack() && canAttack)
            OnAttackAinmation();

        if (_chase.UpdateChase(_detectionRange, _targetPlayer))
            ChasePalyer();
        else
            Patrol();
        _animation.SetSpeed(_mover.Speed);
    }

    private void OnDisable()
    {
        _health.DamageReceived -= OnDamaged;
        _animationEvents.Attacing -= OnAttack;
    }

    public void SetPatrolZone(PatrolZone patrolZone)
    {
        _patrolRoute.SetPatorlZone(patrolZone);
    }

    private void Patrol()
    {
        _mover.Move(ChangeDirection(_patrolRoute.CurrentTarget));

        if (IsTargetReached())
            _patrolRoute.MoveToNext();
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
        _mover.Move(ChangeDirection(_targetPlayer.position));
    }

    private bool ReadyForAnAttack()
    {
        if (_targetPlayer == null)
            return false;

        Vector2 path = (Vector2)_targetPlayer.position - (Vector2)transform.position;

        if (path.sqrMagnitude <= _attackDistance * _attackDistance)
            return true;

        return false;
    }

    private void OnAttackAinmation()
    {
        _animation.TriggerKick();

        _isAttacking = true;
    }

    private void OnAttack()
    {
        if (_targetPlayer == null)
            return;

        _attack.HitPlayer(_targetPlayer);

        _isAttacking = false;
    }

    private void SearchTarget()
    {
        Player playerTarget = _targetDetector.FillingAnOpponent<Player>(_detectionRange);

        if (playerTarget != null)
            _targetPlayer = playerTarget.transform;
    }

    private void OnDamaged()
    {
        _animation.TriggerHit();
    }

    private Vector2 ChangeDirection(Vector2 target)
    {
        return (target - (Vector2)transform.position).normalized;
    }
}