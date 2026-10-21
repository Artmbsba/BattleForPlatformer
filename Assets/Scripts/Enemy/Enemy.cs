using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PatrolRoute))]
[RequireComponent(typeof(MoverEnemy))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(EnemyDeath))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Animations _animation;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private AnimationEvents _animationEvents;

    private PatrolRoute _patrolRoute;
    private MoverEnemy _moverEnemy;
    private EnemyChase _enemyChase;
    private EnemyDeath _enemyDeath;
    private EnemyAttack _enemyAttack;
    private Health _health;
    private Collider2D _playerCollayder;

    private float _facingDirection = 1f;
    private bool _isAttacking;

    public float FacingDirection => _facingDirection;

    private void Awake()
    {
        _patrolRoute = GetComponent<PatrolRoute>();
        _moverEnemy = GetComponent<MoverEnemy>();
        _enemyChase = GetComponent<EnemyChase>();
        _enemyDeath = GetComponent<EnemyDeath>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Damaged += OnDamaged;
        _enemyChase.CanAttacking += OnAttackAinmation;
        _animationEvents.Attacing += OnAttack;
    }

    private void FixedUpdate()
    {
        bool canAttack = _isAttacking == false && _enemyAttack.CanAttack();

        _enemyChase.UpdateChase(canAttack);

        if (_enemyChase.IsChasing)
            ChasePalyer();
        else
            Patrol();
        _animation.SetSpeed(_moverEnemy.Speed);
    }

    private void OnDisable()
    {
        _health.Damaged -= OnDamaged;
        _enemyChase.CanAttacking -= OnAttackAinmation;
        _animationEvents.Attacing -= OnAttack;
    }

    public void SetPatrolZone(PatrolZone patrolZone)
    {
        _patrolRoute.SetPatorlZone(patrolZone);
    }

    private void Patrol()
    {
        Vector2 target = _patrolRoute.CurrentTarget;

        if (_moverEnemy.MoveTo(target))
            _patrolRoute.MoveToNext();

        UpdateRotation(target);
    }

    private void ChasePalyer()
    {
        Vector2 target = _enemyChase.Target.position;

        _moverEnemy.MoveTo(target);

        UpdateRotation(target);
    }

    private void OnAttackAinmation(Collider2D collider)
    {
        _playerCollayder = collider;

        _animation.TriggerKick();

        _isAttacking = true;
    }

    private void OnAttack()
    {
        _enemyAttack.HitPlayer(_playerCollayder);
        _isAttacking = false;
    }

    private void UpdateRotation(Vector2 target)
    {
        float direction = target.x - transform.position.x;

        _facingDirection = Mathf.Sign(direction);

        _spriteRenderer.flipX = _facingDirection < 0f;
        _enemyChase.SetFacingDerection(FacingDirection);
    }

    private void OnDamaged()
    {
        _animation.TriggerHit();
    }
}
