using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _platformMask;
    [SerializeField] private float _rayDistance = 0.6f;

    public bool IsGround(Rigidbody2D rigidbody)
    {
        bool isTouchedGround = false;

        RaycastHit2D hit = Physics2D.Raycast(
             rigidbody.position, Vector2.down, _rayDistance, _platformMask);

        if (hit.collider != null)
            isTouchedGround = true;

        return isTouchedGround;
    }
}