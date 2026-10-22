using System;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] private float _loseRange = 5f;

    public bool UpdateChase(float detectionRange, Transform target)
    {
        bool isChasing = false;

        if (target != null)
        {
            Vector2 toTarget = target.position - transform.position;
            float sqrDistance = toTarget.sqrMagnitude;

            if (sqrDistance < detectionRange * detectionRange)
                isChasing = true;
            else if (sqrDistance > _loseRange * _loseRange)
                isChasing = false;
        }

        return isChasing;
    }
}