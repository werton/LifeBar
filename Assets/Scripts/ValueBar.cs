using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class ValueBar : MonoBehaviour, IValueDrawer
{
    private Slider _slider;

    [SerializeField] private Color _lowHpColor;
    [SerializeField] private Color _highHpColor;
    [SerializeField] private Image _image;

    [SerializeField] private float _sliderSpeed = 30f;

    private float _clientTargetValue;
    private float _clientMaxValue;
    private float _sliderTargetValue;
    private Coroutine _valueStepChanger;

    protected void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    public void OnValueChanged(float clientTargetValue, float clientMaxValue)
    {
        ThrowExeptionOnIncorrectValue(clientTargetValue, clientMaxValue);

        if (_clientTargetValue != clientTargetValue || _clientMaxValue != clientMaxValue)
        {
            StopValueStepChanger();

            _clientTargetValue = clientTargetValue;
            _clientMaxValue = clientMaxValue;
            _sliderTargetValue = ConvertClientValueToSlider(clientTargetValue, clientMaxValue);

            _valueStepChanger = StartCoroutine(SetSliderValueByStep());
        }
    }

    public void SetValueNow(float clientValue, float clientMaxValue)
    {
        ThrowExeptionOnIncorrectValue(clientValue, clientMaxValue);
        StopValueStepChanger();
        SetSliderValueNow(ConvertClientValueToSlider(clientValue, clientMaxValue));
    }

    private void ThrowExeptionOnIncorrectValue(float clientValue, float clientMaxValue)
    {
        if (clientValue > clientMaxValue)
            throw new Exception("value can't be greater than maxValue");

        if (clientValue < 0 || clientMaxValue < 0)
            throw new Exception("value, maxValue can't be negative");
    }

    private IEnumerator SetSliderValueByStep()
    {
        Debug.Log($"beg- cl:{_clientTargetValue}, cl max:{_clientMaxValue}, sl:{_slider.value}, sl target:{_sliderTargetValue}");


        while (_slider.value != _sliderTargetValue)
        {
            SetSliderValueNow(Mathf.MoveTowards(_slider.value, _sliderTargetValue, _sliderSpeed * Time.deltaTime));
            yield return null;
        }

        _valueStepChanger = null;

        Debug.Log($"end- cl:{_clientTargetValue}, cl max:{_clientMaxValue}, sl:{_slider.value}, sl target:{_sliderTargetValue}");

        yield break;
    }

    private void SetSliderValueNow(float value)
    {
        _slider.value = value;
    }

    private void StopValueStepChanger()
    {
        if (_valueStepChanger != null)
        {
            StopCoroutine(_valueStepChanger);
            _valueStepChanger = null;

            _sliderTargetValue = _slider.value;
        }
    }

    private float ConvertClientValueToSlider(float clientValue, float clientMaxValue)
    {
        return clientValue * _slider.maxValue / clientMaxValue;
    }

    public void ChangeSliderColor()
    {
        _image.color = Color.Lerp(_lowHpColor, _highHpColor, _slider.value / 100);
    }
}