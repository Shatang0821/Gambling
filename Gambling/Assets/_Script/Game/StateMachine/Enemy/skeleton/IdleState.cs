using Framework.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
    public class IdleState : BaseState
    {
        public IdleState(string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
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
                ChangeState(StateEnum.Move);
            }
        }
    }
}


