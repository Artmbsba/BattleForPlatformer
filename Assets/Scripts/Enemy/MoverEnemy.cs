using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverEnemy : MonoBehaviour
{
    [SerializeField] private float _reachDistance = 0.5f;
    [SerializeField, Range(0.0f, 3.0f)] private float _speed = 1.0f;

    private Rigidbody2D _rigidbody;

    public float ReachDistance => _reachDistance;
    public float Speed => _speed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.freezeRotation = true;
    }

    public bool MoveTo(Vector2 target)
    {
        Vector2 current = _rigidbody.position;
        Vector2 path = target - current;

        if (path.sqrMagnitude <= _reachDistance * _reachDistance)
            return true;

        float directionX = target.x - current.x;

        _rigidbody.linearVelocity = new Vector2(Mathf.Sign(directionX) * _speed, _rigidbody.linearVelocity.y);

        return false;
    }
}
