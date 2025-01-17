using Framework.Entity;
using FrameWork.Resource;

namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
    public class DamageState : BaseState
    {
        public DamageState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            enemyData = ResManager.Instance.GetAssetCache<EntityData>("EntityData/EnemyData");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (enemyData.HP <= 0)
            {
                ChangeState(StateEnum.Die);
            }
        }
        public override void Exit()
        {
            base.Exit();
        }

    }


}
