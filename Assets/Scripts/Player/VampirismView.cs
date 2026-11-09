using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Slider _Bar;
    [SerializeField] float _speed = 1f;

    private float _minNumberBar = 0f;
    private float _maxNumberBar = 1f;

    public void Tick(float elapsedTime, float duration)
    {
        float targetVolume = elapsedTime / duration;
        _Bar.value = Mathf.MoveTowards(_Bar.value, targetVolume, _speed * Time.deltaTime);
    }

    public void StartAbility(float radius)
    {
        _Bar.value = _minNumberBar;

        _image.enabled = true;
        SetRadius(radius);
    }

    public void StartCooldown()
    {
        _Bar.value = _maxNumberBar;

        _image.enabled = false;
    }


    private void SetRadius(float radius)
    {
        float radiusToDiameter = 2f;

        _image.transform.localScale = new Vector3(radius * radiusToDiameter, radius * radiusToDiameter, 1f);
    }
}
