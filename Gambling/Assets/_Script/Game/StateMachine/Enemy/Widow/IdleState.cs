using Framework.Entity;
using Framework.FSM;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
    public class IdleState : BaseState
    {
        EntityObject owner;
        
        int random;
        float distamce;
        public IdleState(EntityObject entityObject, string animName, MyStateMachine stateMachine, Animator animator) : base(entityObject, animName, stateMachine, animator)
        {
            owner = entityObject;
        }

        public override void Enter()
        {
            base.Enter();
            if (p_entity.Position.x <= owner.Position.x)
            {
                owner.Scale = new Vector3(-1, 1, 1);
            }
            else
            {
                owner.Scale = new Vector3(1, 1, 1);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            distamce = owner.DistanceX(p_entity);
            Debug.Log(distamce);

            if (distamce > 1)
            {
                ChangeState(StateEnum.Move);
            }
            if (distamce < 1) {
                ChangeToSkillState(1002);
            }

            
            
        }

    }
}


