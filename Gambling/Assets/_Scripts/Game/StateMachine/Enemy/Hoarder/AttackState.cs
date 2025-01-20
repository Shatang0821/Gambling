using Framework.Entity;
using UnityEngine;

namespace Game.StateMachine.Enemy.Hoarder
{
    public class AttackState : BaseState
    {
        public AttackState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void Enter()
        {
            base.Enter();
        }
    }


}
