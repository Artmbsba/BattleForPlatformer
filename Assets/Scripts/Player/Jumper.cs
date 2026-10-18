using UnityEngine;

public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
        
    private int _maxJump = 2;
    private int _jumpUsed;

    public bool CanJump => _jumpUsed < _maxJump;
        
    public void Jump(Rigidbody2D rigidbody)
    {
        if (CanJump)
        {
            _jumpUsed++;

            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, _jumpForce);
        }
    }

    public void ResetJumps()
    {
        _jumpUsed = 0;
    }
}
