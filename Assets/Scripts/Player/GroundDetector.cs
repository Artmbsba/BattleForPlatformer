using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _platformMask;
    [SerializeField] private float _rayDistance = 1f;

    public bool IsGround(Rigidbody2D rigidbody)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            rigidbody.position, Vector2.down, _rayDistance, _platformMask);

        return hit.collider != null;
    }
}