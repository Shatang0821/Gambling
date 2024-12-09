using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerStateMachine
{
    public class MoveState : BaseState
    {
        private MovementComponent _movementComponent;
        
        public MoveState(Entity entity, string animName, StateMachine stateMachine, Animator animator) : base(entity, animName, stateMachine, animator)
        {
            _movementComponent = entity.GetEntityComponent<MovementComponent>();
        }
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}