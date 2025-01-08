using Framework.Entity;
using FrameWork.Component;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
    public class WalkState : BaseState
    {
        EntityObject owner;
        MovementComponent _movement;
        Rigidbody2D _rigidbody;
        Vector2 direction;
        public WalkState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject,animName, stateMachine, animator)
        {
            owner = entityObject;
            _movement = owner.GetEntityComponent<MovementComponent>();
            _rigidbody = owner.GetComponent<Rigidbody2D>();
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();
            float distance = owner.DistanceX(p_entity);

            if (distance > 1f)
            {
                _movement.Move(direction,1f);
            }
            else
            {
                
                ChangeState(StateEnum.Idle);
            }
            
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            
        }

        public override void Enter()
        {
            base.Enter();
            direction = new Vector2(owner.Scale.x, 0);
        }

        public override void Exit()
        {
            base.Exit();
            _rigidbody.velocity = Vector2.zero;

        }
    }


}
