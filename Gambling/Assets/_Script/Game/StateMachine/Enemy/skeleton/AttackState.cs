using Framework.Entity;
using UnityEngine;

namespace Game.StateMachine.Enemy.skeleton
{
    public class AttackState : BaseState
    {
        public AttackState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //_attackComponent.DamageFlow();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void Enter()
        {
            base.Enter();
            //_targetSelectorComponent.CloseRangeAttack(entityObject.LocalPosition, 30, 2, 10);
            //_attackComponent.DamageFlow();
        }
    }


}
