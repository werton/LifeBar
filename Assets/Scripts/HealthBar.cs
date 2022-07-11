using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour, IHealthDrawer
{
    private Slider _slider;

    void Awake()
    {
        _slider = GetComponent<Slider>(); 
    }

    void Update()
    {
        
    }

    public void OnHealthChanged(int healthValue)
    {
        Debug.Log("OnHealthChanged called");
        Debug.Log(healthValue);
        //StartCoroutine(SetHealthValue(healthValue));
    }


    //[SerializeField] private Player _player;
    //[SerializeField] private Color _lowHpColor;
    //[SerializeField] private Color _highHpColor;
    //[SerializeField] private Image _image;

    //private float _sliderSpeed = 7f;
    //private float _sliderDefaultValue = 50.0f;

    //private void Start()
    //{
    //    _slider = GetComponent<Slider>();
    //    _slider.value = _sliderDefaultValue;
    //}


    //private IEnumerator SetHealthValue(int healthValue)
    //{
    //    while (_slider.value != _player.Health)
    //    {
    //        _slider.value = Mathf.MoveTowards(_slider.value, _player.Health, _sliderSpeed * Time.deltaTime);
    //        yield return null;
    //    }
    //    yield break;
    //}

    //public void ChangeSliderColor()
    //{
    //    _image.color = Color.Lerp(_lowHpColor, _highHpColor, _slider.value / 100);
    //}

}
