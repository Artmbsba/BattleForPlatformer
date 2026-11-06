using UnityEngine;
using UnityEngine.UI;

public class ViewHealthBarSmooth : ViewHealthBarOrdinary
{
    [SerializeField] private float _speed = 0.5f;

    private void Update()
    {
        HealthBar.value = Mathf.MoveTowards(HealthBar.value, TargetVolume, _speed * Time.deltaTime);
    }
}