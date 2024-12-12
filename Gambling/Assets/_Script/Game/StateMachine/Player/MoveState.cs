using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class MoveState : BaseState
    {
        private MovementComponent _movementComponent;
        
        public MoveState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
            _movementComponent = owner.GetEntityComponent<MovementComponent>();
        }
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (playerInputComponent.DirectionlInput.x == 0)
            {
                ChangeState(StateEnum.Idle);
            }
            
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