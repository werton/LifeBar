public interface IHealthDrawer
{
    public void OnHealthChanged(float health, float maxHealth);

    public void SetValueNow(float health, float maxHealth);
}