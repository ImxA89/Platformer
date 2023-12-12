using UnityEngine;

public class EnemyFollowState : IEnemyState
{
    private EnemyStateMachine _stateMachine;
    private Skin _skin;
    private EnemyMover _enemyMover;
    private Transform _target;
    private Enemy _enemy;
    private Transform _enemyTransform;
    private int _speed;
    private float _distanceToLoseTarget = 7f;
    private float _heightToLoseTarget = 3f;
    private float _distanceToAttack = 1.5f;
    private bool _isLookingLeft;
    private int _speedMultyplier = 2;

    public EnemyFollowState(EnemyStateMachine stateMachine, Skin skin, EnemyMover enemyMover, Enemy enemy, int speed)
    {
        _stateMachine = stateMachine;
        _skin = skin;
        _enemyMover = enemyMover;
        _enemy = enemy;
        _speed = speed;
        _enemyTransform = enemy.transform;
    }

    public void Enter()
    {
        _target = _enemy.Target;
    }

    public void Exit() { }

    public void Update()
    {
        if (_target != null && (Vector3.Distance(_target.position, _enemyTransform.position) < _distanceToLoseTarget)
            && Mathf.Abs(_target.position.y - _enemyTransform.position.y) < _heightToLoseTarget)
        {
            if (Vector3.Distance(_target.position, _enemyTransform.position) <= _distanceToAttack)
                _stateMachine.SetState(typeof(EnemyAttackState));

            _enemyMover.Move(_target.position, _speed*_speedMultyplier);
            LookAtTargetDirection();
        }
        else
        {
            _stateMachine.SetState(typeof(EnemyPatrolState));
        }
    }

    private void LookAtTargetDirection()
    {
        if (_target.position.x > _enemyTransform.position.x)
            _isLookingLeft = false;
        else
            _isLookingLeft = true;

        _skin.Turn(_isLookingLeft);
    }
}
