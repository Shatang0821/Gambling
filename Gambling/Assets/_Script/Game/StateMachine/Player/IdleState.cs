using Framework.Entity;
using Framework.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    
    public class IdleState : BaseState
    {
        
        public IdleState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (playerInputComponent.DirectionlInput.x != 0)
            {
                ChangeState(StateEnum.Move);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}