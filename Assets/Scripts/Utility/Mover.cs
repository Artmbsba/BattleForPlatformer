using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField, Range(0f, 3f)] private float _moveSpeed = 3f;
    [SerializeField] private Transform _veiwTransform;

    private Rigidbody2D _rigidbody;
    private float _facingfDeretion = 1f;

    public float Speed => _moveSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 1f;
        _rigidbody.freezeRotation = true;
        _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    public float MovePlayer(Vector2 moveInput)
    {
        float direction = moveInput.x;

        _rigidbody.linearVelocity = new Vector2(direction * _moveSpeed, _rigidbody.linearVelocity.y);

        _facingfDeretion = Mathf.Sign(direction);

        if (direction == 0)
            return Mathf.Abs(direction) * _moveSpeed;

        Vector3 scale = _veiwTransform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(direction);
        _veiwTransform.localScale = scale;


        return Mathf.Abs(direction) * _moveSpeed;
    }

    public void MoveEnemy(Vector2 target)
    {
        Vector2 current = _rigidbody.position;

        float directionX = target.x - current.x;

        _rigidbody.linearVelocity = new Vector2(Mathf.Sign(directionX) * _moveSpeed, _rigidbody.linearVelocity.y);

        if (directionX == 0)
            return;

        Vector3 scale = _veiwTransform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(directionX);
        _veiwTransform.localScale = scale;
    }

    public float GetFacingDerection()
    {
        return _facingfDeretion;
    }
}
