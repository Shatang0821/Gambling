using Framework.Entity;
using FrameWork.Resource;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.EnemyWidow.StateEnum;
    public class DamagedState : BaseState
    {
        public DamagedState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            // if (enemyData.HP <= 0)
            // {
            //     ChangeState(StateEnum.Die);
            // }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (stateTimer > 0.3f)
            {
                ChangeState(StateEnum.Idle);
            }
        }
        public override void Exit()
        {
            base.Exit();
        }

    }


}
