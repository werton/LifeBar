using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue = 100f;

    public event Action<float, float> HealthChanged;

    public float Value { get; set; }
    public float MaxValue
    { get { return _maxValue; } set { _maxValue = value; } }
    public float PreviousValue { get; private set; }

    [SerializeField]
    private MonoBehaviour _healthDrawer;

    private IHealthDrawer HealthDrawer => (IHealthDrawer)_healthDrawer;

    private void OnValidate()
    {
        if (_healthDrawer is IHealthDrawer)
            return;

        Debug.LogError(_healthDrawer.name + " needs to implement " + nameof(IHealthDrawer));
        _healthDrawer = null;
    }

    private void OnEnable()
    {
        Value = MaxValue;

        HealthChanged += HealthDrawer.OnHealthChanged;
        HealthDrawer.SetValueNow(Value, MaxValue);
    }

    private void OnDisable()
    {
        HealthChanged -= HealthDrawer.OnHealthChanged;
    }

    public void Add(int addingValue)
    {
        Debug.Log("add");
        ThrowExecptionOnNegative(addingValue);

        if (Value == MaxValue || addingValue == 0)
        {
            return;
        }

        Debug.Log(addingValue);

        SetValue(Value + addingValue);
        Debug.Log(Value);
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
            throw new Exception("Value can't be negative");
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