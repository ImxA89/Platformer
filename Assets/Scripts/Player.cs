using System;
using UnityEngine;

[RequireComponent(typeof(Skin))]
public class Player : MonoBehaviour, IAttackable
{
    [SerializeField][Min(2)] private int _maxHealth;
    [SerializeField][Min(2)] private int _damage;

    private Health _health;
    private DamageDealler _damageDealler;
    private Skin _skin;
    private int _enemyLayer = 6;

    public int MaxHealth => _maxHealth;

    public event Action BananaTaken;
    public event Action<int> HealthChanged;

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _damageDealler = new DamageDealler(_damage, _enemyLayer);
        _skin = GetComponent<Skin>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Banana>(out Banana banana))
        {
            Destroy(banana.gameObject);
            BananaTaken?.Invoke();
        }

        if (collision.TryGetComponent<AidKit>(out AidKit aidkit))
        {
            _health.TakeHeal(aidkit.HealPower);
            Destroy(aidkit.gameObject);
        }
    }

    public void TakeHeal(int healPower)
    {
            _health.TakeHeal(healPower);
    }

    public void TakeDamage(int damage)
    {
        _skin.PlayDamageTakenAnimation();
        _health.TakeDamage(damage);
    }

    public void Attack(bool isLookingLeft)
    {
       _damageDealler.MakeDamage(isLookingLeft,transform);
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }

    private void OnHealthChanged(int currentHealth)
    {
        HealthChanged?.Invoke(currentHealth);
    }
}
