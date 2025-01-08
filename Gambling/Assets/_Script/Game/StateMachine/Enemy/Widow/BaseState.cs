using System;
using Framework.Entity;
using Framework.FSM;
using Game.Component;
using Game.Entity;
using Game.Input;
using UnityEngine;

namespace Game.StateMachine.Enemy
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class BaseState : MyAnimationState
    {
        protected EntityObject owner;
        protected SkillComponent skillComponent;
        protected static int SkillID;

        public GameObject player;
        public EntityObject p_entity;
        public BaseState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(animName, stateMachine, animator)
        {
            this.owner = owner;
            skillComponent = this.owner.GetEntityComponent<SkillComponent>();
        }

        public override void Enter()
        {
            base.Enter();
            player = GameObject.FindGameObjectWithTag("Player");
            p_entity = player.GetComponent<EntityObject>();
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
    }
}