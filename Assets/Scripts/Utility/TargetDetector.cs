using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public Collider2D FillingAnOpponent(float facingDirection, float radius)
    {
        Collider2D raycastHit = Physics2D.OverlapCircle(transform.position, radius);

        return raycastHit;
    }
}