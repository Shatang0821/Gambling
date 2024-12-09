using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.PlayerStateMachine
{
    public class RunState : BaseState
    {
        public RunState(string animName, Framework.FSM.StateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
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
                ChangeState(PlayerStateEnum.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}