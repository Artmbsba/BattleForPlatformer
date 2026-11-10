using UnityEngine;

[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField, Range(0f, 3f)] private float _moveSpeed = 3f;
    [SerializeField] private Transform _veiwTransform;

    private Rotator _rotator;
    private Rigidbody2D _rigidbody;

    public float Speed => _moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 1f;
        _rigidbody.freezeRotation = true;
        _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;

        _rotator = GetComponent<Rotator>();
    }

    public float Move(Vector2 direction)
    {
        float directionX = direction.x;

        _rigidbody.linearVelocity = new Vector2(directionX * _moveSpeed, _rigidbody.linearVelocity.y);

        if (directionX == 0)
            return Mathf.Abs(directionX) * _moveSpeed;

        _rotator.Rotation(directionX);

        return Mathf.Abs(directionX) * _moveSpeed;
    }
}