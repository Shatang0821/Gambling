using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Enemy.skeleton
{
    public class IdleState : BaseState
    {
        public IdleState(string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}


