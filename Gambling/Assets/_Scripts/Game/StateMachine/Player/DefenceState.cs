using Framework.Entity;
using Framework.FSM;
using Game.Components;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class DefenceState : BaseState
    {
        private DefendComponent _defendComponent;
        public DefenceState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
            _defendComponent = owner.GetEntityComponent<DefendComponent>();
        }

        public override void Enter()
        {
            base.Enter();
            _defendComponent.StartDefend();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            _defendComponent.SetCanParry(stateTimer); 
            if (_defendComponent.SuccessParry)
            {
                ChangeToSkillState(1006);
            }
            
            if (stateTimer > .2f && !playerInputComponent.DefenceInput)
            {
                ChangeState(StateEnum.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
            _defendComponent.StopDefend();
        }
    }
}