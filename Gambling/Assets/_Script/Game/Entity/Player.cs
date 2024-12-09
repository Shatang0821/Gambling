using FrameWork.Component;
using Framework.Entity;
using PlayerStateMachine;
using UnityEngine;

public enum PlayerStateEnum
{
    Idle,
    Move,
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
        AddEntityComponent<MovementComponent>(new MovementComponent());
     
        _playerStateMachine = CreateStateMachine();
        AddEntityComponent<EntityStateMachine>(_playerStateMachine);
        _playerStateMachine.Initialize(PlayerStateEnum.Idle);

    }

    protected EntityStateMachine CreateStateMachine()
    {
        var stateMachine = new EntityStateMachine();
        var animator = GetComponentInChildren<Animator>();
        stateMachine.RegisterState(PlayerStateEnum.Idle, new IdleState(this,PlayerStateEnum.Idle.ToString(),stateMachine,animator));
        stateMachine.RegisterState(PlayerStateEnum.Move, new MoveState(this,PlayerStateEnum.Move.ToString(),stateMachine,animator));
        
        return stateMachine;
    }

    //一時的に使う
    private void Update()
    {
        _playerStateMachine.LogicUpdate();
    }
    
    
}