using System;

public interface IAttackable
{
    public int MaxHealth { get; }

    public event Action<int> HealthChanged;

    public void TakeDamage(int damage);
}