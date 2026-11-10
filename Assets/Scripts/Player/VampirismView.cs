using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Image _image;
    [SerializeField] private Slider _bar;
    [SerializeField] private float _speed = 1f;

    private float _minNumberBar = 0f;
    private float _maxNumberBar = 1f;

    private void OnEnable()
    {
        _vampirism.AbilityStarted += OnAbilityStarted;
        _vampirism.Ticked += OnTicked;
        _vampirism.CooldownStarted += OnCooldownStarted;
    }

    private void OnDisable()
    {
        _vampirism.AbilityStarted -= OnAbilityStarted;
        _vampirism.Ticked -= OnTicked;
        _vampirism.CooldownStarted -= OnCooldownStarted;
    }

    public void OnTicked(float elapsedTime, float duration)
    {
        float targetVolume = elapsedTime / duration;
        _bar.value = Mathf.MoveTowards(_bar.value, targetVolume, _speed * Time.deltaTime);
    }

    public void OnAbilityStarted(float radius)
    {
        _bar.value = _minNumberBar;

        _image.enabled = true;
        SetRadius(radius);
    }

    public void OnCooldownStarted()
    {
        _bar.value = _maxNumberBar;

        _image.enabled = false;
    }


    private void SetRadius(float radius)
    {
        float radiusToDiameter = 2f;

        _image.transform.localScale = new Vector3(radius * radiusToDiameter, radius * radiusToDiameter, 1f);
    }
}
