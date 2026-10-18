using UnityEngine;

public class TargetDetector: MonoBehaviour
{
    [SerializeField] private float _hitDistance = 1f;
    [SerializeField] private float _beamDisplacement = 0.33f;

    public Collider2D FillingAnOpponent(float facingDirection)
    {
        Vector2 direction = Vector2.right * facingDirection;
        Vector2 origin = (Vector2)transform.position + direction * _beamDisplacement;

        RaycastHit2D raycastHit = Physics2D.Raycast(origin, direction, _hitDistance);

        return raycastHit.collider;
    }
}