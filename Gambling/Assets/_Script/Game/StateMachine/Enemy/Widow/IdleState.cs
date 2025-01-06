using Framework.Entity;
using Framework.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
    public class IdleState : BaseState
    {
        EntityObject owner;
        int random;
        GameObject player;
        public IdleState(EntityObject entityObject, string animName, MyStateMachine stateMachine, Animator animator) : base(entityObject, animName, stateMachine, animator)
        {
            owner = entityObject;
            
        }

        public override void Enter()
        {
            base.Enter();
            random = Random.RandomRange(1, 5);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //switch(random)
            //{
            //    case 1:
            //        ChangeToSkillState(1001);
            //        break;
            //    case 2:
            //        ChangeToSkillState(1002);
            //        break;
            //    case 3:
            //        ChangeToSkillState(1003);
            //        break;
            //    case 4:
            //        ChangeToSkillState(1004);
            //        break;
            //    case 5:
            //        ChangeToSkillState(1005);
            //    break;

            //}
            ChangeToSkillState(1002);
        }

    }
}


