using UnityEngine;
using AnimationState = Framework.FSM.AnimationState;

namespace Game.PlayerStateMachine
{
    public class BaseState : AnimationState
    {
        public BaseState(string animName, Framework.FSM.StateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Enter :" + GetType());
        }
    }
}