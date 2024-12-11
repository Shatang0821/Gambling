using System;
using EnemyStateMachine;
using Framework.Entity;
using UnityEngine;
using Game.StateMachine;
using Game.StateMachine.Enemy.skeleton;

namespace Game.Entity
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
        stateMachine.RegisterState(EnemyStateEnum.Idle, new IdleState(EnemyStateEnum.Idle.ToString(), stateMachine, animator));
        stateMachine.RegisterState(EnemyStateEnum.Move, new WalkState(EnemyStateEnum.Move.ToString(), stateMachine, animator));

        return stateMachine;
    }

    private void Update()
    {
        _enemyStateMachine.LogicUpdate();
        
    }
}