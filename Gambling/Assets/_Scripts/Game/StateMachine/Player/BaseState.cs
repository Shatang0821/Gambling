using System;
using Framework.Entity;
using Framework.FSM;
using Game.Component;
using Game.Components;
using Game.Input;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class BaseState : MyAnimationState
    {
        protected EntityObject owner;
        protected HealthComponent healthComponent;
        protected DefendComponent defendComponent;
        protected PlayerInputComponent playerInputComponent;
        protected SkillComponent skillComponent;
        
        protected static int SkillID;
        public BaseState(EntityObject owner,string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            this.owner = owner;
            playerInputComponent = owner.GetEntityComponent<PlayerInputComponent>();
            skillComponent = owner.GetEntityComponent<SkillComponent>();
            healthComponent = owner.GetEntityComponent<HealthComponent>();
            defendComponent = owner.GetEntityComponent<DefendComponent>();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (healthComponent.GetFlag("TakenDamage"))
            {
                TakenDamage();
                healthComponent.SetFlag("TakenDamage",false);
            }
            
            if (healthComponent.GetFlag("Die"))
            {
                if(stateMachine.CurrentState != stateMachine.GetState(StateEnum.Die.ToString()))
                    ChangeState(StateEnum.Die);
            }
        }

        public override void Exit()
        {
            base.Exit();
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

        protected void TakenDamage()
        {
            if (stateMachine.CurrentState != stateMachine.GetState(StateEnum.Defence.ToString()))
            {
                ChangeState(StateEnum.Damaged);
            }
            else
            {
                AnimatorUtility.Blink(owner.GetComponentInChildren<SpriteRenderer>(),0.2f,0.1f);
            }
            
        }
    }
}