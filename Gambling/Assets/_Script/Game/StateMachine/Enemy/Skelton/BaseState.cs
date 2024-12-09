using Framework.FSM;
using UnityEngine;
using AnimationState = Framework.FSM.AnimationState;

namespace EnemyStateMachine
{
    public class BaseState : AnimationState
    {
        public BaseState(string animName, StateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
        }
        
    }
}