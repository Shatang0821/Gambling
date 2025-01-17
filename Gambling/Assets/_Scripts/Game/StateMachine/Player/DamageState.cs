using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
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