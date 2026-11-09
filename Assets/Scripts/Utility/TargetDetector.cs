using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public T FillingAnOpponent<T>(float radius) where T : Component
    {
        Collider2D[] raycastHits = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach(var hit in raycastHits)
           if( hit.TryGetComponent(out T target))
                return target;

        return null;
    }

    public T FindClosest<T>(float radius) where T: Component
    {
        Collider2D[] raycastHits = Physics2D.OverlapCircleAll(transform.position, radius);

        T closestTarget = null;
        float minDistance = Mathf .Infinity;

        foreach (var hit in raycastHits)
        {
            T target = hit.GetComponent<T>();

           if(target == null)
                continue;

            float distance = Vector2.Distance(transform.position,target.transform.position);

            if(distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target;
            }
        }
        
        return closestTarget;
    }
}