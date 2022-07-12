using System;
public interface IDrawableValue
{
    public event Action<float, float> ValueChanged;
}