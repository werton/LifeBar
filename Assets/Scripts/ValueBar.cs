
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ValueBar : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(ShowValue);
    }

    private void Update()
    {
    }
    public void IncreaseValue(int value)
    {
        _slider.value += 10;
    }

    public void DecreaseValue(int value)
    {
        _slider.value -= 10;
    }

    private void ShowValue(float value)
    {
        Debug.Log(_slider.value);
    }
 
}


