using System;
using FrameWork.Component;
using Framework.Entity;
using UnityEngine;
using Game.StateMachine;
using Game.StateMachine.Player;
using Modules.Input;
using Unity.VisualScripting;

namespace Game.Entity
{
    public class Player : EntityObject
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
            AddEntityComponent<MovementComponent>(new MovementComponent());
     
            _playerStateMachine = CreateStateMachine();
            AddEntityComponent<EntityStateMachine>(_playerStateMachine);
            _playerStateMachine.Initialize(StateEnum.Idle);
            

        }
        
        protected EntityStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityStateMachine();
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new IdleState(this,StateEnum.Idle.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Move, new MoveState(this,StateEnum.Move.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Attack, new AttackState(this,StateEnum.Attack.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Damaged, new DamageState(this,StateEnum.Damaged.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Die, new DieState(this,StateEnum.Die.ToString(),stateMachine,animator));
        
            return stateMachine;
        }

        //一時的に使う
        private void Update()
        {
            _playerStateMachine.LogicUpdate();
        }
    
    
    }
}
