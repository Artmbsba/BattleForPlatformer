using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float _detectionRange = 5f;
    [SerializeField] private float _loseRange = 7f;

    private Transform _target;
    private bool _isChasing;

    public bool IsChasing => _isChasing;

    public void UpdateChase()
    {
        float distance = Vector2.Distance(transform.position, _target.position);

        if (IsChasing == false && distance < _detectionRange)
            _isChasing = true;
        else if (IsChasing && distance > _loseRange)
            _isChasing = false;
    }

    public void SetPalyerPosistion(Transform target)
    {
        _target = target;
    }
}