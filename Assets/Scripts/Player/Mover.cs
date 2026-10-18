using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3f;

    private SpriteRenderer _spriteRenderer;
    private float _facingfDeretion = 1f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float Move(Vector2 moveInput, Rigidbody2D rigidbody)
    {
        float direction = moveInput.x;

        rigidbody.linearVelocity = new Vector2(direction * _moveSpeed, rigidbody.linearVelocity.y);

        _facingfDeretion = Mathf.Sign(direction);

        _spriteRenderer.flipX = _facingfDeretion < 0f;

        return Mathf.Abs(direction) * _moveSpeed;
    }

    public float GetFacingDerection()
    {
        return _facingfDeretion;
    }
}
