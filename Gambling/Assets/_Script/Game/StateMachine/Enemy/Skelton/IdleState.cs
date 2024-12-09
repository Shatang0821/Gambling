using Framework.FSM;
using UnityEngine;

namespace EnemyStateMachine
{
    public class IdleState : BaseState
    {
        public IdleState(string animName, StateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}


