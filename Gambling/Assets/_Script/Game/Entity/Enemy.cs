using Framework.Entity;
using UnityEngine;
using Game.StateMachine;
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
            Damaged,
            Die
        }

        private EntityMyStateMachine _enemyMyStateMachine;

        private void Awake()
        {
            _enemyMyStateMachine = CreateStateMachine();
            AddEntityComponent<EntityMyStateMachine>(_enemyMyStateMachine);
            _enemyMyStateMachine.Initialize(StateEnum.Idle);
        }

        protected EntityMyStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityMyStateMachine();
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new IdleState(StateEnum.Idle.ToString(),stateMachine,animator));
        
            return stateMachine;
        }

        private void Update()
        {
            _enemyMyStateMachine.LogicUpdate();
        }
    } 
}


