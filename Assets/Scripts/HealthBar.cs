using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour, IHealthDrawer
{
    private Slider _slider;

    [SerializeField] private Color _lowHpColor;
    [SerializeField] private Color _highHpColor;
    [SerializeField] private Image _image;

    [SerializeField] private float _flowSpeed = 20f;

    private float _sliderDefaultValue = 50;

    protected void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _sliderDefaultValue;
    }

    public void OnHealthChanged(float health, float maxHealth)
    {
        StartCoroutine(SetSliderValueByStep(health, maxHealth));
    }

    public void SetValueNow(float health, float maxHealth)
    {
        SetSliderValue(health, maxHealth);
    }

    private IEnumerator SetSliderValueByStep(float sliderNewValue, float maxValue)
    {
        while (_slider.value != sliderNewValue)
        {
            SetSliderValue(Mathf.MoveTowards(_slider.value, sliderNewValue, _flowSpeed * Time.deltaTime), maxValue);
            yield return null;
        }

        yield break;
    }

    private void SetSliderValue(float value, float maxValue)
    {
        _slider.value = _slider.maxValue * GetValueRatio(value, maxValue);
    }

    private float GetValueRatio(float value, float maxValue)
    {
        if (value > maxValue)
            throw new Exception("value can't be greater than maxValue");

        if (value < 0 || maxValue < 0)
            throw new Exception("value, maxValue can't be negative");

        return value / maxValue;
    }

    public void ChangeSliderColor()
    {
        _image.color = Color.Lerp(_lowHpColor, _highHpColor, _slider.value / 100);
    }
}