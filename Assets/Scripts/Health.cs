using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]


public class Health : MonoBehaviour
{
    [SerializeField] private int _maxValue = 100;

    //[SerializeField] private GameObject _HealthDrawer;

    private IHealthDrawer _drawer;

    public event Action<int> HealthChanged;

    public int Value { get; set; }
    public int MaxValue { get { return _maxValue; } set { _maxValue = value; } }
    public int PreviousValue { get; private set; }

    [SerializeField]
    private MonoBehaviour  _HealthDrawer;
    private IHealthDrawer HealthDrawerInterface => (IHealthDrawer) _HealthDrawer;

    private void OnValidate()
    {
        if (_HealthDrawer is IHealthDrawer)
            return;

        Debug.LogError(_HealthDrawer.name + " needs to implement " + nameof(IHealthDrawer));
        _HealthDrawer = null;
    }

    private void OnEnable()
    {
        if (_HealthDrawer.TryGetComponent<IHealthDrawer>(out _drawer))
        {
            HealthChanged += _drawer.OnHealthChanged;
        }

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
