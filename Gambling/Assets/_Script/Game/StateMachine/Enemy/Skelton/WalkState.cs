namespace EnemyStateMachine
{
    public class WalkState : BaseState
    {
        public WalkState(string animName, Framework.FSM.StateMachine stateMachine, UnityEngine.Animator animator) : base(animName, stateMachine, animator)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }
    }


}
