using System.Collections;
using UnityEngine;

public class ViewHealthBarSmooth : ViewHealthBarOrdinary
{
    [SerializeField] private float _speed = 0.5f;

    private Coroutine _animationCorutine;

    protected override void DrawHealth()
    {
        transform.localPosition = Offset;
        TargetVolume = Health.CurrentValue / Health.MaxValue;

        if (_animationCorutine != null)
            StopCoroutine(_animationCorutine);

        _animationCorutine = StartCoroutine(AnimationHP());
    }

    private IEnumerator AnimationHP()
    {
        while (Mathf.Approximately(HealthBar.value, TargetVolume) == false)
        {
            HealthBar.value = Mathf.MoveTowards(HealthBar.value, TargetVolume, _speed * Time.deltaTime);

            yield return null;
        }

        HealthBar.value = TargetVolume;

        _animationCorutine = null;
    }
}