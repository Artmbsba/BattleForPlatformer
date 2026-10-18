using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    [SerializeField] private PatrolPoint[] _points;

    private int _currentIndex;
    private PatrolZone _patrolZone;

    public Vector2 CurrentTarget => _points[_currentIndex].transform.position;

    public void SetPatorlZone(PatrolZone patrolZone)
    {
        _patrolZone = patrolZone;
        _currentIndex = 0;

        CollectPoints();
    }

    public void MoveToNext()
    {
        _currentIndex = ++_currentIndex % _points.Length;
    }

    private void CollectPoints()
    {
        List<PatrolPoint> points = new List<PatrolPoint>();

        for (int i = 0; i < _patrolZone.transform.childCount; i++)
        {
            Transform child = _patrolZone.transform.GetChild(i);
            
            if (child.TryGetComponent(out PatrolPoint point))
                points.Add(point);
        }

        _points = points.ToArray();
    }
}
