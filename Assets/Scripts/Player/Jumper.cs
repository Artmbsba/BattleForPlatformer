using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;

   [SerializeField] private GroundDetector _groundDetector;
    private Rigidbody2D _rigidbody;

    private int _maxJump = 2;
    private int _jumpUsed;
    private bool _wasGrounded;
    private bool _isGrounded;

    public event Action Jumped;
    public event Action Landed;

    public bool CanJump => _jumpUsed < _maxJump;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        bool _isGrounded = _groundDetector.IsGround(_rigidbody);
        
        if(_isGrounded && _wasGrounded)
            Landed?.Invoke();

        if(_isGrounded == false && _wasGrounded)
            Jumped?.Invoke();

        _wasGrounded = _isGrounded;
    }

    public void Jump()
    {
        _isGrounded = _groundDetector.IsGround(_rigidbody);

        if (_isGrounded)
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
