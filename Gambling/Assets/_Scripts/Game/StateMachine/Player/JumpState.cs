using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class JumpState : AirState
    {
        private float _velY = 7.5f;
        public JumpState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
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
            if (stateTimer > 0.2f)
            {
                if (!playerInputComponent.JumpInput)
                {
                    ChangeState(StateEnum.Fall);
                }
            }
            if (movementComponent.GetVel.y < 0)
            {
                ChangeState(StateEnum.Fall);
            }
        }

        public override void Exit()
        {
            base.Exit();
            movementComponent.SetVelocityY(0);
        }
    }
}