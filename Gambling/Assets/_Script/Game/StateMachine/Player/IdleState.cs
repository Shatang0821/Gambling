using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;
using UnityEngine.InputSystem;
using AnimationState = Framework.FSM.AnimationState;


namespace PlayerStateMachine
{
    public class IdleState : BaseState
    {
        
        public IdleState(Entity entity, string animName, StateMachine stateMachine, Animator animator) : base(entity, animName, stateMachine, animator)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                ChangeState(PlayerStateEnum.Move);
            }

            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                ChangeState(PlayerStateEnum.Move);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}