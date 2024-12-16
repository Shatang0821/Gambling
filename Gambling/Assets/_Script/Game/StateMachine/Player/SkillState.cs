using Framework.Entity;
using Framework.FSM;
using Game.Component;
using Game.SkillSystem;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class SkillState : BaseState
    {
        //private SkillData _skillData;               //現在スキルデータ
        private SkillComponent _skillComponent;
        private SkillProcessor _skillProcessor;

        private SkillData _skillData;
        public SkillState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
            _skillComponent = owner.GetEntityComponent<SkillComponent>();
            _skillProcessor = new SkillProcessor(base.owner);
        }

        public override void Enter()
        {
            // スキルを取得する
            var skill  = _skillComponent.GetSkill(1001);
            if (!skill.IsReady())
            {
                Debug.Log("aaa");
            }
            _skillData = skill.GetSkillData();
            // アニメーションを指定する
            stateHash = Animator.StringToHash(skill.AnimationName);
            _skillProcessor.SetSkill(skill);
            
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            _skillProcessor.Update(stateTimer);
            
            if (stateTimer > _skillData.Duration)
            {
                ChangeState(StateEnum.Idle);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
        }
    }
}