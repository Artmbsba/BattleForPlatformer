using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PatrolRoute))]
[RequireComponent(typeof(MoverEnemy))]
[RequireComponent(typeof(EnemyChase))]
[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(EnemyDeath))]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private PatrolRoute _patrolRoute;
    private MoverEnemy _moverEnemy;
    private EnemyChase _enemyChase;
    private Animation _animation;
    private EnemyDeath _enemyDeath;
    private EnemyAttack _enemyAttack;
    private Health _health;

    private Transform _targetPlayer;

    private float _facingDirection = 1f;

    public float FacingDirection => _facingDirection;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _patrolRoute = GetComponent<PatrolRoute>();
        _moverEnemy = GetComponent<MoverEnemy>();
        _enemyChase = GetComponent<EnemyChase>();
        _animation = GetComponent<Animation>();
        _enemyDeath = GetComponent<EnemyDeath>();
        _enemyAttack = GetComponent<EnemyAttack>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Damaged += OnDamaged;
    }

    private void FixedUpdate()
    {
        _enemyChase.UpdateChase();

        if (_enemyChase.IsChasing)
            ChasePalyer();
        else
            Patrol();

        _animation.SetSpeed(_moverEnemy.Speed);
    }

    private void OnDisable()
    {
        _health.Damaged -= OnDamaged;
    }

    public void SetPatrolZone(PatrolZone patrolZone)
    {
        _patrolRoute.SetPatorlZone(patrolZone);
    }

    public void SetTargetPlayer(Transform targetPlayer)
    {
        _targetPlayer = targetPlayer;
        _enemyChase.SetPalyerPosistion(targetPlayer);
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
        Vector2 target = _targetPlayer.position;

        _moverEnemy.MoveTo(target);

        UpdateRotation(target);
    }

    private void UpdateRotation(Vector2 target)
    {
        float direction = target.x - transform.position.x;

        _facingDirection = Mathf.Sign(direction);

        _spriteRenderer.flipX = _facingDirection < 0f;
        _enemyAttack.SetFacingDerection(FacingDirection);
    }

    private void OnDamaged()
    {
        _animation.TriggerHit();
    }
}
