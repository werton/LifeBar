using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar: MonoBehaviour
{
    [SerializeField] private PositiveNumber _maxValue;

    private PositiveNumber _value;

    //private PositiveNumber _maxValue;

//    public public class HealthBar : MonoBehaviour
//()
//    {

//    }

//    public public class HealthBar : MonoBehaviour
//(int maxValue) : this()
//    {
//        MaxValue = maxValue;
//    }


    public int Value { get { return _value.Get(); } set { _value.Set(value); } }
    public int MaxValue { get { return _maxValue.Get(); } private set { _maxValue.Set(value); } }

    public event Action ValueChanged;

    public void Add(int value)
    {
        if (Value == MaxValue || value <= 0)
        {
            return;
        }

        Value += value;
        ValueChanged?.Invoke();
    }

    public void Reduce(int value)
    {
        if (Value == 0 || value <= 0)
        {
            return;
        }

        Value -= value;
        Value = Mathf.Clamp(Value, 0, MaxValue);
        ValueChanged?.Invoke();
    }

}

public struct PositiveNumber
{
    private int _value;

    public PositiveNumber(int value) : this()
    {
        Set(value);
    }

    public void Set(int value)
    {
        if (value < 0)
        {
            throw new Exception("PositiveNumber can't be less than zero");
        }
    }

    public int Get()
    {
        return _value;
    }

    public event Action ValueChanged;

    public static int operator +(PositiveNumber number, int value)
    {
        return number.Get() + value;
    }

    public static int operator +(PositiveNumber number1, PositiveNumber number2)
    {
        return number1.Get() + number2.Get();
    }
}

