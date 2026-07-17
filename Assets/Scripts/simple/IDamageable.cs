using System;

public interface IDamageable
{
    public void TakeDamage(float damage = 1);
    public event EventHandler OnTakeDamage;
    public event EventHandler HealthChange;
}
