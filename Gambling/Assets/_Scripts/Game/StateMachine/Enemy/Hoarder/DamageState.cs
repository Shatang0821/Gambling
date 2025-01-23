using Framework.Entity;
using FrameWork.Resource;

namespace Game.StateMachine.Enemy.Hoarder
{
    using StateEnum = Game.Entity.EnemyHoarder.StateEnum;
    public class DamageState : BaseState
    {
        public DamageState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            if (owner.MyData.HP <= 0)
            {
                ChangeState(StateEnum.Die);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (stateTimer > 0.6f)
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
