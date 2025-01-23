using Framework.Entity;

namespace Game.StateMachine.Enemy.skeleton
{
    public class DieState : BaseState
    {
        private bool isDied = false;
        public DieState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
            defendComponent.StartInvincible();
            isDied = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (stateTimer > 0.8f && !isDied)
            {
                isDied = true;
                healthComponent.Die();
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void Exit()
        {
            base.Exit();
            defendComponent.StopInvincible();
        }
    }


}
