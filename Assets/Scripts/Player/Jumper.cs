using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;

   [SerializeField] private GroundDetector _groundDetector;
    private Rigidbody2D _rigidbody;

    private int _maxJump = 2;
    private int _jumpUsed;

    public bool CanJump => _jumpUsed < _maxJump;
    public bool IsTouchedGround => (_groundDetector.IsGround(_rigidbody));

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if (IsTouchedGround)
            ResetJumps();

        if (CanJump)
        {
            _jumpUsed++;

            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, _jumpForce);
        }
    }

    private void ResetJumps()
    {
        _jumpUsed = 0;
    }
}
