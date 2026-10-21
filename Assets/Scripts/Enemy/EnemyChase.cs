using System;
using UnityEngine;

[RequireComponent(typeof(TargetDetector))]
public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float _attackDistance = 0.5f;
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _loseRange = 5f;

    private TargetDetector _targetDetector;
    private Transform _target;
    private bool _isChasing;
    private float _facingDirection = 1f;

    public event Action<Collider2D> CanAttacking;

    public bool IsChasing => _isChasing;
    public Transform Target => _target;

    private void Awake()
    {
        _targetDetector = GetComponent<TargetDetector>();
    }

    public void UpdateChase(bool canAttack)
    {
        SearchTarget();
        UpdateChaseDecision();
        NotifyIfCanAttack(canAttack);
    }

    private void SearchTarget()
    {
        Collider2D playerCollaider = _targetDetector.FillingAnOpponent(_facingDirection, _detectionRange);

        if (playerCollaider != null && playerCollaider.TryGetComponent(out Player player))
            _target = player.transform;      
    }

    private void UpdateChaseDecision()
    {
        if (_target == null)
        {
            _isChasing = false;

            return;
        }

        Vector2 toTarget = _target.position - transform.position;
        float sqrDistance = toTarget.sqrMagnitude;

        if (sqrDistance < _detectionRange * _detectionRange)
            _isChasing = true;
        else if (sqrDistance > _loseRange * _loseRange)
            _isChasing = false;
    }

    private void NotifyIfCanAttack(bool canAttack)
    {
        Collider2D attackCollider = _targetDetector.FillingAnOpponent(_facingDirection, _attackDistance);

        if (attackCollider != null && attackCollider.TryGetComponent<Player>(out _) && canAttack)
            CanAttacking?.Invoke(attackCollider);
    }

    public void SetFacingDerection(float facingDirection)
    {
        _facingDirection = facingDirection;
    }
}