using System;
using UnityEngine;

public class Health : MonoBehaviour, IDrawableValue
{
    [SerializeField]
    private float _maxValue = 100f;

    public event Action<float, float> ValueChanged;

    public float Value { get; private set; }

    public float MaxValue
    {
        get { return _maxValue; }
        private set { _maxValue = value; }
    }

    public float PreviousValue { get; private set; }

    private void Start()
    {
        SetValue(MaxValue);
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
            ValueChanged?.Invoke(Value, MaxValue);
        }
    }
}