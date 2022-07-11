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
    }

}
