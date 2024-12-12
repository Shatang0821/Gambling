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
        private PlayerInputComponent _playerInputComponent;
        
        //一時的に使う
        private void Awake()
        {
                                    AddEntityComponent(new MovementComponent());
            _playerInputComponent = AddEntityComponent(new PlayerInputComponent());
            
            // ステートマシンは最後に生成
            _playerStateMachine = CreateStateMachine();
            _playerStateMachine.InitState(StateEnum.Idle);
        }
        
        //一時的に使う
        private void Update()
        {
            _playerStateMachine.LogicUpdate();
        }


        private void OnEnable()
        {
            _playerInputComponent.OnEnable();
        }

        private void OnDisable()
        {
            _playerInputComponent.OnDisable();
        }

        /// <summary>
        /// 制作メソッド 後ほど制作工場に依頼すること
        /// </summary>
        /// <returns></returns>
        protected EntityStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityStateMachine(this);
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new IdleState(this,StateEnum.Idle.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Move, new MoveState(this,StateEnum.Move.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Attack, new AttackState(this,StateEnum.Attack.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Damaged, new DamageState(this,StateEnum.Damaged.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Die, new DieState(this,StateEnum.Die.ToString(),stateMachine,animator));
        
            return stateMachine;
        }


    
    }
}
