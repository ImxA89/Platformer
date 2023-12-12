using System.Threading.Tasks;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    private Skin _skin;
    private EnemyStateMachine _stateMachine;
    private DamageDealler _damageDealler;
    private Transform _target;
    private Enemy _enemy;
    private Transform _enemyTransform;
    private bool _inThisState;
    private bool _isLookingLeft;
    private int _animationTime = 350;
    private int _attackStartDelay = 150;
    private float _attackRange = 1.5f;
    private float _heightToAttack = 0.5f;

    public EnemyAttackState(Skin skin, EnemyStateMachine stateMachine, DamageDealler damageDealler, Enemy enemy)
    {
        _skin = skin;
        _stateMachine = stateMachine;
        _damageDealler = damageDealler;
        _enemy = enemy;
        _enemyTransform = _enemy.transform;
    }

    public void Enter()
    {
        _isLookingLeft = _skin.IsLookingLeft;
        _inThisState = true;
        _target = _enemy.Target;

        Attack();
    }

    public void Exit()
    {
        _inThisState = false;
    }

    public void Update()
    {
        if (_target == null || Vector3.Distance(_target.position, _enemyTransform.position) > _attackRange
            || Mathf.Abs(_target.position.y - _enemyTransform.position.y) > _heightToAttack)
            _stateMachine.SetState(typeof(EnemyFollowState));
    }

    private async void Attack()
    {
        while (_inThisState && _enemyTransform != null)
        {
            _skin.PlayAttackeAnimation();
            await Task.Delay(_attackStartDelay);

            if (_enemyTransform != null)
                _damageDealler.MakeDamage(_isLookingLeft, _enemyTransform);

            await Task.Delay(_animationTime - _attackStartDelay);
        }
    }
}
