using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform _veiwTransform;

    public void Rotation(float directionX)
    {
        float reversalInDegrees = 180f;

        float angle = 0;

        if (directionX < 0)
            angle = reversalInDegrees;

        _veiwTransform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}