using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]


public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 100;

    [SerializeField] private GameObject _HealthDrawer;

    //[SerializeField] private HealthDrawerInterface _HealthDrawer;


    private IHealthDrawer _drawer;

    //private int _value;
    //private int _previosValue;

    public event Action<int> HealthChanged;

    public int Value { get; set; }
    public int MaxValue { get { return _maxValue; } set { _maxValue = value; } }
    public int PreviousValue { get; private set; }

    private void OnEnable()
    {


        _drawer = _HealthDrawer.GetComponent<IHealthDrawer>();
        HealthChanged += _drawer.OnHealthChanged;

        //HealthChanged += _HealthDrawer.Implmentation.OnHealthChanged;

        //HealthChanged += _HealthDrawer.Implmentation.OnHealthChanged;

         //HealthChanged += _HealthDrawer._implmentation.OnHealthChanged;


        Value = MaxValue;

    }

    public void Add(int addingValue)
    {
        ThrowExecptionOnNegative(addingValue);

        if (Value == MaxValue || addingValue == 0)
        {
            return;
        }

        SetValue(Value + addingValue);
    }

    public void Reduce(int reducingValue)
    {
        ThrowExecptionOnNegative(reducingValue);

        if (Value == 0 || reducingValue == 0)
        {
            return;
        }

        SetValue(Value - reducingValue);
    }

    //public void Reduce10()
    //{
    //    Debug.Log(Value);
    //    SetValue(Value - 10);
    //}

    private static void ThrowExecptionOnNegative(int value)
    {
        if (value < 0)
            throw new Exception("Value can't be negative");
    }

    private void SetValue(int newValue)
    {
        PreviousValue = Value;
        Value = newValue;
        Value = Mathf.Clamp(Value, 0, MaxValue);

        if (PreviousValue != Value)
        {
            HealthChanged?.Invoke(Value);
        }
    }
}
