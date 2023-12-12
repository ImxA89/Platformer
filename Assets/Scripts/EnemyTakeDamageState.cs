using System.Threading.Tasks;

public class EnemyTakeDamageState : IEnemyState
{
    private Skin _skin;
    private EnemyStateMachine _stateMachine;
    private int _delayTime = 100;

    public EnemyTakeDamageState(Skin skin, EnemyStateMachine stateMachine)
    {
        _skin = skin;
        _stateMachine = stateMachine;
    }

    public void Enter()
    {
        _skin.PlayDamageTakenAnimation();
        ReturnStateWithDelay();
    }

    public void Exit() {}

    public void Update() {}

    private async void ReturnStateWithDelay()
    {
        await Task.Delay(_delayTime);
        _stateMachine.SetLastState();
    }
}
