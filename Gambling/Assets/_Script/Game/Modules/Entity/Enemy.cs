using Framework.Entity;
using Game.StateMachine;
using UnityEngine;
using Game.StateMachine.Enemy.skeleton;
using FrameWork.Component;

namespace Game.Entity
{
    public class Enemy : EntityObject
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
        
        private EntityStateMachine _enemyStateMachine;
    
        private void Awake()
        {
            var movementComponent = new MovementComponent();
            AddEntityComponent<MovementComponent>(movementComponent);
            movementComponent.Initialize(this);

            
            _enemyStateMachine = CreateStateMachine();
            _enemyStateMachine.InitState(StateEnum.Idle);
        }

        protected EntityStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityStateMachine(this);
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new IdleState(this,StateEnum.Idle.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Move, new WalkState(this, StateEnum.Move.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Attack, new AttackState(this,StateEnum.Attack.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Damaged, new DamageState(this,StateEnum.Damaged.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Die, new DeathState(this,StateEnum.Die.ToString(), stateMachine, animator));

            return stateMachine;
        }

        private void Update()
        {
            _enemyStateMachine.LogicUpdate();
        
        }
    }
}