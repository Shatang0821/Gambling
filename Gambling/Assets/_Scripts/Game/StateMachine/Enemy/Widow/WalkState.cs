using Framework.Entity;
using FrameWork.Component;
using FrameWork.Resource;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.EnemyWidow.StateEnum;
    public class WalkState : BaseState
    {
        MovementComponent _movement;
        Rigidbody2D _rigidbody;
        Vector2 direction;
        public WalkState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject,animName, stateMachine, animator)
        {
            _movement = owner.GetEntityComponent<MovementComponent>();
            _rigidbody = owner.GetComponent<Rigidbody2D>();
        }



        public override void LogicUpdate()
        {
            base.LogicUpdate();
            float distance = owner.DistanceX(p_entity);

            if (distance > attackrange)
            {
                _movement.Move(direction,3f);
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
