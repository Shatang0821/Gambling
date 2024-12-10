using FrameWork.Component;
using Framework.Entity;
using UnityEngine;
using Game.StateMachine;
using Game.StateMachine.Player;

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
        
        private EntityMyStateMachine _playerMyStateMachine;

        //一時的に使う
        private void Awake()
        {
            AddEntityComponent<MovementComponent>(new MovementComponent());
     
            _playerMyStateMachine = CreateStateMachine();
            AddEntityComponent<EntityMyStateMachine>(_playerMyStateMachine);
            _playerMyStateMachine.Initialize(StateEnum.Idle);

        }

        protected EntityMyStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityMyStateMachine();
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new IdleState(this,StateEnum.Idle.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Move, new MoveState(this,StateEnum.Move.ToString(),stateMachine,animator));
        
            return stateMachine;
        }

        //一時的に使う
        private void Update()
        {
            _playerMyStateMachine.LogicUpdate();
        }
    
    
    }
}
