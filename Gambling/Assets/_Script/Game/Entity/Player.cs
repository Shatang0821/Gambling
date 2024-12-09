using Framework.Entity;
using PlayerStateMachine;
using UnityEngine;

public enum PlayerStateEnum
{
    Idle,
    Run,
    Attack,
    Dash,
    Damaged,
    Die
}

public class Player : Entity
{
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
        var animator = GetComponentInChildren<Animator>();
        stateMachine.RegisterState(PlayerStateEnum.Idle, new IdleState(PlayerStateEnum.Idle.ToString(),stateMachine,animator));
        stateMachine.RegisterState(PlayerStateEnum.Run, new RunState(PlayerStateEnum.Run.ToString(),stateMachine,animator));
        
        stateMachine.Initialize(PlayerStateEnum.Idle);
        
        return stateMachine;
    }

    //一時的に使う
    private void Update()
    {
        _playerStateMachine.LogicUpdate();
    }
}