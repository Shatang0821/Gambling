using Framework.Entity;
using Framework.FSM;
using Modules.Input;
using UnityEngine;

namespace Game.StateMachine.Player
{
    public class BaseState : MyAnimationState
    {
        protected EntityObject owner;
        protected PlayerInputComponent playerInputComponent;
        public BaseState(EntityObject owner,string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            this.owner = owner;
            playerInputComponent = owner.GetEntityComponent<PlayerInputComponent>();
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}