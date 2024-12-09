using Framework.FSM;
using System.Diagnostics;
using UnityEngine;

namespace StateMachine.Enemy
{
    public class WalkState : Framework.FSM.AnimationState
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
