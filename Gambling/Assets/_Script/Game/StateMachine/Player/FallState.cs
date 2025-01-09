using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class FallState : AirState
    {
        private float _velY = -3.0f;
        public FallState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
        }

        public override void Enter()
        {
            base.Enter();
            movementComponent.SetVelocityY(_velY);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (owner.IsGroundDetected())
            {
                ChangeState(StateEnum.Idle);
            }
        }
        
        public override void Exit()
        {
            base.Exit();
            movementComponent.SetVelocityY(0);
        }
    }
}