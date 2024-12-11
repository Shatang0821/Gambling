using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
    public class AttackState : BaseState
    {

        public AttackState(EntityObject entityObject, string animName, MyStateMachine stateMachine, Animator animator) : base(entityObject, animName, stateMachine, animator)
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