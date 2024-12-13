using Framework.Entity;
using FrameWork.Component;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
    public class WalkState : BaseState
    {
        Vector2 speed;
        float timer;
        public WalkState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject,animName, stateMachine, animator)
        {
            speed = new Vector2(-1, 0);
            timer = 0;
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();
            _movementComponent.Move(speed, 5.0f, 1.0f, true);

            
            
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            
        }
    }


}
