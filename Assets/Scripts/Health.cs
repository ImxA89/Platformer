using System;

public class Health
{
    private int _maxHealth;
    private int _currentHealth;

    public Health(int maxHealth)
    {
        _maxHealth = maxHealth;
        _currentHealth = maxHealth;
    }

    public event Action Died;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        _currentHealth -= damage;

        if (_currentHealth < 0)
            Died?.Invoke();
    }

    public void TakeHeal(int healPower)
    {
        if (healPower < 0)
            return;

        _currentHealth += healPower;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
}
