using Framework.Entity;

namespace Game.StateMachine.Enemy.skeleton
{
    public class DamageState : BaseState
    {
        public DamageState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }
    }


}
