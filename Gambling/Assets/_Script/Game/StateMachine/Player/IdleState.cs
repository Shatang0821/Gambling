using Framework.Entity;
using Framework.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    
    public class IdleState : BaseState
    {
        
        public IdleState(EntityObject entityObject, string animName, MyStateMachine stateMachine, Animator animator) : base(entityObject, animName, stateMachine, animator)
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

            if (Keyboard.current.dKey.wasPressedThisFrame)
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