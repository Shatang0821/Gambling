using System;
using EnemyStateMachine;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;


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
        _enemyStateMachine.Initialize(EnemyStateEnum.Idle);
    }

    protected EntityStateMachine CreateStateMachine()
    {
        var stateMachine = new EntityStateMachine();
        var animator = GetComponentInChildren<Animator>();
        stateMachine.RegisterState(EnemyStateEnum.Idle, new IdleState(EnemyStateEnum.Idle.ToString(),stateMachine,animator));
        
        return stateMachine;
    }

    private void Update()
    {
        _enemyStateMachine.LogicUpdate();
    }
}
