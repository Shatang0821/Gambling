using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
    public class BaseState : MyAnimationState
    {
        protected EntityObject entityObject;
        public BaseState(EntityObject entityObject,string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            this.entityObject = entityObject;
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}