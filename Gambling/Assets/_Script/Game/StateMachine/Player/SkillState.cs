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
        public SkillState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
            _skillComponent = owner.GetEntityComponent<SkillComponent>();
        }

        public override void Enter()
        {
            // スキルを取得する
            //_skillData = _skillComponent.GetSkillData("Attack01");
            // アニメーションを指定する
           // stateHash = Animator.StringToHash(_skillData.AnimationName);
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (stateTimer > 1.0f)
            {
                ChangeState(StateEnum.Idle);
            }
        }
    }
}