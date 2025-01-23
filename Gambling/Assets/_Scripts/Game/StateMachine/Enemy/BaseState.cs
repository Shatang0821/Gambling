using System;
using Framework.Entity;
using Framework.FSM;
using FrameWork.Resource;
using Game.Component;
using Game.Components;
using Game.Entity;
using Game.Input;
using UnityEngine;

namespace Game.StateMachine.Enemy
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
    public class BaseState : MyAnimationState
    {
        protected EntityObject owner;
        protected SkillComponent skillComponent;
        protected DefendComponent defendComponent;
        protected HealthComponent healthComponent;
        protected static int SkillID;

        public GameObject player;
        public EntityObject p_entity;
        //public EntityData enemyData;
        public float MaxHp = 100;
        public float attackrange = 4.5f; //攻撃に入る距離

        public BaseState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            this.owner = owner;
            skillComponent = this.owner.GetEntityComponent<SkillComponent>();
            healthComponent = this.owner.GetEntityComponent<HealthComponent>();
            defendComponent = owner.GetEntityComponent<DefendComponent>();
        }

        public override void Enter()
        {
            base.Enter();
            player = GameObject.FindGameObjectWithTag("Player");
            p_entity = player.GetComponent<EntityObject>();
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
            AnimatorUtility.Blink(owner.GetComponentInChildren<SpriteRenderer>(),0.3f,0.15f);
            
        }
    }
}