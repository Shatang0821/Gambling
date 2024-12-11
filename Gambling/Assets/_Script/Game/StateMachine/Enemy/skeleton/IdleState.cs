using Framework.Entity;
using Framework.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
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

            if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                ChangeState(StateEnum.Attack);
            }

            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                ChangeState(StateEnum.Damaged);
            }

            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                ChangeState(StateEnum.Die);
            }
        }
    }
}


