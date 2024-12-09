using System;
using Framework.Entity;
using Framework.FSM;


public enum EnemyStateEnum
{
    Idle,
    Move,
    Attack,
    Damaged,
    Die
}

public class Enemy : Entity
{


    private EntityStateMachine _enemyStateMachine;

    private void Awake()
    {
        _enemyStateMachine = CreateStateMachine();
        AddEntityComponent<EntityStateMachine>(_enemyStateMachine);
    }

    protected EntityStateMachine CreateStateMachine()
    {
        var stateMachine = new EntityStateMachine();
        //stateMachine.RegisterState(StateEnum.Idle, );
        return stateMachine;
    }

    private void Update()
    {
        _enemyStateMachine.LogicUpdate();
    }
}
