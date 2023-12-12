using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyPatrolState : IEnemyState
{
    private Queue<Transform> _waypoints = new Queue<Transform>();
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private Transform _enemyTransform;
    private Skin _skin;
    private EnemyMover _enemyMover;
    private Transform _currentWaypoint;
    private float _searchDistance = 7f;
    private float _distanceToReachWaypoint = 0.3f;
    private int _rayCastDelayTime = 500;
    private int _targetLayerMask;
    private int _speed;
    private bool _inThisState;
    private bool _isLookingLeft = false;

    public EnemyPatrolState(Transform[] waypoints, Enemy enemy, Skin skin, EnemyMover enemyMover,
        int targetLayer, int speed, EnemyStateMachine stateMachine)
    {
        for (int i = 0; i < waypoints.Length; i++)
            _waypoints.Enqueue(waypoints[i]);

        _enemy = enemy;
        _skin = skin;
        _enemyMover = enemyMover;
        _targetLayerMask = 1 << targetLayer;
        _speed = speed;
        _enemyTransform = _enemy.transform;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _enemy.TakeTarget(null);
        _inThisState = true;
        ChangeWaypoint();
        LookForPlayer();
    }

    public void Exit()
    {
        _inThisState = false;
    }

    public void Update()
    {
        _enemyMover.Move(_currentWaypoint.position, _speed);

        if (Vector3.Distance(_currentWaypoint.position, _enemyTransform.position) < _distanceToReachWaypoint)
            ChangeWaypoint();
    }

    private void ChangeWaypoint()
    {
        _currentWaypoint = _waypoints.Dequeue();
        _waypoints.Enqueue(_currentWaypoint);

        if (_currentWaypoint.position.x < _enemyTransform.position.x)
            _isLookingLeft = true;
        else 
            _isLookingLeft = false;

        _skin.Turn(_isLookingLeft);
    }

    private async void LookForPlayer()
    {
        while (_inThisState)
        {
            Vector2 searchDirection;

            if (_isLookingLeft)
                searchDirection = -_enemyTransform.right;
            else
                searchDirection = _enemyTransform.right;

            RaycastHit2D hit = Physics2D.Raycast(_enemyTransform.position,searchDirection,_searchDistance,_targetLayerMask);

            if (hit && hit.transform.TryGetComponent<Player>(out Player player))
            {
                _enemy.TakeTarget(player.transform);
                _stateMachine.SetState(typeof(EnemyFollowState));
            }
                
            await Task.Delay(_rayCastDelayTime);
        }
    }
}
