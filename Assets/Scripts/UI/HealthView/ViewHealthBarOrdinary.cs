using UnityEngine;
using UnityEngine.UI;

public class ViewHealthBarOrdinary: ViewBar
{
    [SerializeField] protected Slider HealthBar;
    [SerializeField] protected Vector3 Offset = new Vector3(0f,0.23f,0f);

    protected float TargetVolume;

    protected override void DrawHealth()
    {
        TargetVolume = Health.CurrentHealth/ Health.MaxHealth;

        HealthBar.value = TargetVolume;
        transform.localPosition = Offset;
    }
}