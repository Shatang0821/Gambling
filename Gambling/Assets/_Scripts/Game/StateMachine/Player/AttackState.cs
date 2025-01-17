using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class AttackState : BaseState
    {

        public AttackState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
            
        }
        public override void Enter()
        {
            base.Enter();
            
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (stateTimer < 0.2f)
            {
                if (playerInputComponent.DefenceInput)
                {
                    ChangeState(StateEnum.Defence);
                }
            }
            if (stateTimer > 0.4f)
            {
                ChangeState(StateEnum.Idle);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}