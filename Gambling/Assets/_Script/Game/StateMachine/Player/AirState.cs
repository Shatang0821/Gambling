using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class AirState : BaseState
    {
        protected MovementComponent movementComponent;
        public AirState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
            movementComponent = owner.GetEntityComponent<MovementComponent>();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (playerInputComponent.AttackInput)
            {
                ChangeToSkillState(1002);
            }
        }
        
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            movementComponent.SetVelocityX(playerInputComponent.DirectionlInput.x * 5.0f);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}