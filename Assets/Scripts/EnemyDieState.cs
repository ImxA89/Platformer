using System.Threading.Tasks;
using UnityEngine;

public class EnemyDieState : IEnemyState
{
    private GameObject _enemy;
    private int _destroyDelay = 200;

    public EnemyDieState (GameObject enemy)
    {
        _enemy = enemy;
    }

    public async void Enter()
    {
        await Task.Delay(_destroyDelay);
        GameObject.Destroy(_enemy);
    }

    public void Exit(){ }

    public void Update() { }
}
