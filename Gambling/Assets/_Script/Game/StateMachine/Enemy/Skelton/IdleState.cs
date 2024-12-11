using Framework.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EnemyStateMachine
{
    public class IdleState : BaseState
    {
        public IdleState(string animName, StateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                ChangeState(EnemyStateEnum.Move);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

}


