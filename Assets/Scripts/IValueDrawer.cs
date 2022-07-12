public interface IValueDrawer
{
    public void OnValueChanged(float value, float maxValue);

    public void SetValueNow(float value, float maxValue);
}