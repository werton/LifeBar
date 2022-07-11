using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]


public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue;
    [SerializeReference] private IHealthDrawer _drawer;

    private int _value;
    private int _previosValue;

    public event Action ValueChanged;

    public int Value
    {
        get { return _value; }

        set { _value = GetPositive(value); }
    }

    public int MaxValue
    {
        get { return _maxValue; }

        set { _value = GetPositive(value); }
    }

    public int PreviosValue
    {
        get { return _previosValue; }

        private set { _value = GetPositive(value); }
    }

    public void Add(int value)
    {
        if (Value == MaxValue || value <= 0)
        {
            return;
        }

        SetValue(Value + value);
    }

    public void Reduce(int value)
    {
        if (Value == 0 || value <= 0)
        {
            return;
        }

        SetValue(Value - value);
    }

    private static int GetPositive(int value)
    {
        if (value < 0)
        {
            throw new Exception("Value can't be negative");
        }

        return value;
    }

    private void SetValue(int value)
    {
        PreviosValue = Value;
        Value = value;
        Value = Mathf.Clamp(Value, 0, MaxValue);

        if (PreviosValue != Value)
        {
            ValueChanged?.Invoke();
        }
    }
}
