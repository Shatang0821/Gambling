using Framework.Entity;
using Framework.FSM;
using Game.SkillSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    
    public class GroundState : BaseState
    {
        
        public GroundState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (playerInputComponent.JumpInput)
            {
                ChangeState(StateEnum.Jump);
            }

            if (!owner.IsGroundDetected())
            {
                ChangeState(StateEnum.Fall);
            }

            if (playerInputComponent.AttackInput)
            {
                ChangeToSkillState(1002);
                return;
            }

            if (playerInputComponent.DefenceInput)
            {
                ChangeState(StateEnum.Defence);
                return;
            }

            if (playerInputComponent.DashInput)
            {
                ChangeToSkillState(1001);
                return;
            }
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}