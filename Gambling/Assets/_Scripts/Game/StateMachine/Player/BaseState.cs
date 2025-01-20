using System;
using Framework.Entity;
using Framework.FSM;
using Game.Component;
using Game.Input;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class BaseState : MyAnimationState
    {
        protected EntityObject owner;
        protected HealthComponent healthComponent;
        protected PlayerInputComponent playerInputComponent;
        protected SkillComponent skillComponent;
        
        protected static int SkillID;
        public BaseState(EntityObject owner,string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            this.owner = owner;
            playerInputComponent = owner.GetEntityComponent<PlayerInputComponent>();
            skillComponent = this.owner.GetEntityComponent<SkillComponent>();
            
            healthComponent = this.owner.GetEntityComponent<HealthComponent>();
            healthComponent.AddDamageAction(TakenDamage);
        }

        public override void Enter()
        {
            base.Enter();
        }
        
        /// <summary>
        /// スキル状態遷移するための専用メソッド
        /// </summary>
        /// <param name="skillID"></param>
        protected void ChangeToSkillState(int skillID)
        {
            SkillID = skillID;
            if (skillComponent.IsSkillReady(skillID))
            {
                ChangeState(StateEnum.Skill);
            }
        }

        protected void TakenDamage(float damage)
        {
            ChangeState(StateEnum.Damaged);
        }
    }
}