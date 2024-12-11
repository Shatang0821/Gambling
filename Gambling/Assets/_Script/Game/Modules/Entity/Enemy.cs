using Framework.Entity;
using Game.StateMachine;
using UnityEngine;
using Game.StateMachine.Enemy.skeleton;

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
            _enemyStateMachine = CreateStateMachine();
            AddEntityComponent<EntityStateMachine>(_enemyStateMachine);
            _enemyStateMachine.Initialize(StateEnum.Idle);
        }

        protected EntityStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityStateMachine();
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new IdleState(StateEnum.Idle.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Move, new WalkState(StateEnum.Move.ToString(), stateMachine, animator));

            return stateMachine;
        }

        private void Update()
        {
            _enemyStateMachine.LogicUpdate();
        
        }
    }
}