using System;
using Framework.Entity;
using Framework.FSM;

public class Player : Entity
{
    public enum StateEnum
    {
        Idle,
        Move,
        Attack,
        Dash,
        Damaged,
        Die
    }
    
    private EntityStateMachine _playerStateMachine;

    //一時的に使う
    private void Awake()
    {
        _playerStateMachine = CreateStateMachine();
        AddEntityComponent<EntityStateMachine>(_playerStateMachine);
    }

    protected EntityStateMachine CreateStateMachine()
    {
        var stateMachine = new EntityStateMachine();
        //stateMachine.RegisterState(StateEnum.Idle, );
        return stateMachine;
    }
}