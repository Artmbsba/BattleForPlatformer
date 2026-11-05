using UnityEngine;

public class BarFollow : MonoBehaviour
{
    [SerializeField] Vector3 _offcet = new Vector3(0f, 0.5f, 0f);

    private Transform _target;

    public void Setup(Transform target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = _target.position + _offcet;
        transform.rotation = Camera.main.transform.rotation;
    }
}