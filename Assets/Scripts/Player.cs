using System;
using UnityEngine;

[RequireComponent(typeof(Skin))]
public class Player : MonoBehaviour, IAttackable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;

    private Health _health;
    private DamageDealler _damageDealler;
    private Skin _skin;
    private int _enemyLayer = 6;

    public event Action BananaTaken;

    private void OnValidate()
    {
        if(_maxHealth <= 0)
            _maxHealth = 1;

        if (_damage <= 0)
            _damage = 1;
    }

    private void Awake()
    {
        _health = new Health(_maxHealth);
        _damageDealler = new DamageDealler(_damage, _enemyLayer);
        _skin = GetComponent<Skin>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
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
}
