using Framework.Entity;

namespace Game.StateMachine.Enemy.skeleton
{
    public class DeathState : BaseState
    {
        public DeathState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

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
