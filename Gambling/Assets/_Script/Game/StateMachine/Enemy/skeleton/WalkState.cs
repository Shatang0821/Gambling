using Framework.Entity;
using FrameWork.Component;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.StateMachine.Enemy.skeleton
{
    public class WalkState : BaseState
    {
        Vector2 speed;
        public WalkState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject,animName, stateMachine, animator)
        {
            speed = new Vector2(-3, 0);
            
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _movementComponent.Move(speed,true,5.0f,3.0f);
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
