using System;
using System.Collections.Generic;

public class EnemyStateMachine
{
    private Dictionary<Type, IEnemyState> _states = new Dictionary<Type, IEnemyState>();
    private IEnemyState _currentState;
    private IEnemyState _lastState;

    public void Update()
    {
        _currentState?.Update();
    }

    public void OnDisable()
    {
        _currentState?.Exit();
    }

    public void AddState(IEnemyState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState(Type type)
    {
        if (_currentState?.GetType() == type)
            return;

        if (_states.TryGetValue(type, out IEnemyState newState))
        {
            if (_currentState != null)
            {
                _lastState = _currentState;
                _currentState.Exit();
            }

            _currentState = newState;
            _currentState.Enter();
        }
    }

    public void SetStateByDefault()
    {
        SetState(typeof(EnemyPatrolState));
    }

    public void SetLastState()
    {
        SetState(_lastState.GetType());
    }
}
