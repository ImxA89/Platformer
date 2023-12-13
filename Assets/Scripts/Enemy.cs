using UnityEngine;

[RequireComponent(typeof(Skin))]
public class Enemy : MonoBehaviour, IAttackable
{
    [SerializeField] private Transform[] _points;
    [SerializeField][Min(2)] private int _speed;
    [SerializeField][Min(10)] private int _maxHealth;
    [SerializeField][Min(2)] private int _damage;

    private EnemyStateMachine _enemyStateChanger;
    private EnemyMover _mover;
    private Skin _skin;
    private Health _health;
    private DamageDealler _damageDealler;
    private Transform _target;
    private int _playerLayer = 7;

    public Transform Target => _target;

    private void Awake()
    {
        _skin = GetComponent<Skin>();
        _mover = new EnemyMover(transform);
        _health = new Health(_maxHealth);
        _damageDealler = new DamageDealler(_damage, _playerLayer);
        _enemyStateChanger = new EnemyStateMachine();

        _enemyStateChanger.AddState(new EnemyAttackState(_skin,_enemyStateChanger,_damageDealler, this));
        _enemyStateChanger.AddState(new EnemyTakeDamageState(_skin,_enemyStateChanger));
        _enemyStateChanger.AddState(new EnemyFollowState(_enemyStateChanger,_skin,_mover,this,_speed));
        _enemyStateChanger.AddState(new EnemyPatrolState(_points, this, _skin,_mover,_playerLayer,_speed,_enemyStateChanger));

        _enemyStateChanger.SetStateByDefault();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
        _enemyStateChanger.OnDisable();
    }

    private void Update()
    {
        _enemyStateChanger.Update();
    }

    public void TakeTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    public void TakeDamage(int damage)
    {
        _enemyStateChanger.SetState(typeof(EnemyTakeDamageState));
        _health.TakeDamage(damage);
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }
}