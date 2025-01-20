using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using Game.Component;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class DamageState : BaseState
    {
        public DamageState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {

        }
        public override void Enter()
        {
            base.Enter();

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (stateTimer > 0.6f)
            {
                ChangeState(StateEnum.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}