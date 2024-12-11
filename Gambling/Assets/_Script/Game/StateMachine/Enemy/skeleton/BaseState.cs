using Framework.Entity;
using Framework.FSM;
using FrameWork.Component;
using UnityEngine;

namespace Game.StateMachine.Enemy.skeleton
{
    public class BaseState : MyAnimationState
    {
        protected EntityObject entityObject;
        protected MovementComponent _movementComponent;
        public BaseState(EntityObject entityObject,string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            this.entityObject = entityObject;
            this._movementComponent = entityObject.GetEntityComponent<MovementComponent>();
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}