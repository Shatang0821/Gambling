using Framework.Entity;
using Framework.FSM;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class DefenceState : BaseState
    {
        public DefenceState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (stateTimer > .2f)
            {
                ChangeState(StateEnum.Idle);
            }
        }
    }
}