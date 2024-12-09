using FrameWork.Component;
using UnityEngine;
using UnityEngine.InputSystem;
using AnimationState = Framework.FSM.AnimationState;


namespace Game.PlayerStateMachine
{
    public class IdleState : BaseState
    {
        public IdleState(string animName, Framework.FSM.StateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Debug.Log("In Idle");

            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                ChangeState(PlayerStateEnum.Run);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}