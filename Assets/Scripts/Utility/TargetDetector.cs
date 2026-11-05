using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public Collider2D FillingAnOpponent<T>(float facingDirection, float radius) where T : Component
    {
        Collider2D[] raycastHits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(var hit in raycastHits)
           if( hit.TryGetComponent<T>(out _))
                return hit;

        return null;
    }
}