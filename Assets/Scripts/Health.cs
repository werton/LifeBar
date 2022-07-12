using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float _maxValue = 100f;

    [SerializeField]
    private MonoBehaviour _healthDrawer;

    public event Action<float, float> HealthChanged;

    public float Value { get; private set; }

    public float MaxValue
    {
        get { return _maxValue; }
        private set { _maxValue = value; }
    }

    public float PreviousValue { get; private set; }

    private IValueDrawer HealthDrawer => (IValueDrawer)_healthDrawer;

    private void OnValidate()
    {
        if (_healthDrawer is IValueDrawer)
        {
            return;
        }

        Debug.LogError(_healthDrawer.name + " needs to implement " + nameof(IValueDrawer));
        _healthDrawer = null;
    }

    private void OnEnable()
    {
        Value = MaxValue;

        HealthChanged += HealthDrawer.OnValueChanged;

        HealthDrawer.SetValueNow(Value, MaxValue);
    }

    private void OnDisable()
    {
        HealthChanged -= HealthDrawer.OnValueChanged;
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

    private static void ThrowExecptionOnNegative(int value)
    {
        if (value < 0)
        {
            throw new Exception("Value can't be negative");
        }
    }

    private void SetValue(float newValue)
    {
        PreviousValue = Value;
        Value = newValue;
        Value = Mathf.Clamp(Value, 0, MaxValue);

        if (PreviousValue != Value)
        {
            HealthChanged?.Invoke(Value, MaxValue);
        }
    }
}