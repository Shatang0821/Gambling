using Framework.Entity;
using UnityEngine;
using AnimationState = Framework.FSM.AnimationState;

namespace PlayerStateMachine
{
    public class BaseState : AnimationState
    {
        protected Entity entity;
        public BaseState(Entity entity,string animName, Framework.FSM.StateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            this.entity = entity;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enter :" + GetType());
        }
    }
}