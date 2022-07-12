using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ValueBar : MonoBehaviour
{
    [SerializeField]
    private float _sliderSpeed = 30f;

    [SerializeField]
    private MonoBehaviour _drawableValue;

    private Slider _slider;
    private float _clientTargetValue;
    private float _clientMaxValue;
    private float _sliderTargetValue;
    private Coroutine _valueStepChanger;
    private IDrawableValue DrawableValue => (IDrawableValue)_drawableValue;

    private void OnValidate()
    {
        if (_drawableValue is IDrawableValue)
        {
            return;
        }

        Debug.LogError(_drawableValue.name + " needs to implement " + nameof(IDrawableValue));
        _drawableValue = null;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable ()
    {
        DrawableValue.ValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        DrawableValue.ValueChanged -= OnValueChanged;
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

    private void ThrowExeptionOnIncorrectValue(float clientValue, float clientMaxValue)
    {
        if (clientValue > clientMaxValue)
        {
            throw new Exception("value can't be greater than maxValue");
        }

        if (clientValue < 0 || clientMaxValue < 0)
        {
            throw new Exception("value, maxValue can't be negative");
        }
    }

    private IEnumerator SetSliderValueByStep()
    {
        while (_slider.value != _sliderTargetValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _sliderTargetValue, _sliderSpeed * Time.deltaTime);
            yield return null;
        }

        yield break;
    }

    private void StopValueStepChanger()
    {
        if (_valueStepChanger != null)
        {
            StopCoroutine(_valueStepChanger);
        }
    }

    private float ConvertClientValueToSlider(float clientValue, float clientMaxValue)
    {
        return clientValue * _slider.maxValue / clientMaxValue;
    }
}