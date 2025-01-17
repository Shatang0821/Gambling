using Framework.Entity;
using Framework.FSM;
using Game.Component;
using Game.Input;
using Game.SkillSystem;
using UnityEngine;

namespace Game.StateMachine.Player
{
    using StateEnum = Game.Entity.Player.StateEnum;
    public class SkillState : BaseState
    {
        //private SkillData _skillData;               //現在スキルデータ

        private SkillProcessor _skillProcessor;

        private SkillData _skillData;
        private int _currentSkillID;
        public SkillState(EntityObject owner, string animName, MyStateMachine stateMachine, Animator animator) : base(owner, animName, stateMachine, animator)
        {
            skillComponent = owner.GetEntityComponent<SkillComponent>();
            _skillProcessor = new SkillProcessor(base.owner);
        }

        public override void Enter()
        {
            _currentSkillID = SkillID;
            // スキルを取得する
            var skill  = skillComponent.GetSkill(SkillID);
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

            foreach (var comboData in _skillData.ComboDatas)
            {
                if (stateTimer >= comboData.StartTime && stateTimer <= comboData.EndTime)
                {
                    if (CheckInput(comboData.InputKey))
                    {
                        ChangeToSkillState(comboData.NextSkillID);
                    }
                }
            }
            
            if (stateTimer > _skillData.Duration)
            {
                if (!owner.IsGroundDetected())
                {
                    ChangeState(StateEnum.Fall);
                }
                if (playerInputComponent.DirectionlInput.x == 0)
                {
                    ChangeState(StateEnum.Idle);
                }
                else
                {
                    ChangeState(StateEnum.Move);
                }
                
            }
        }

        public override void Exit()
        {
            base.Exit();
            if (_skillProcessor.IsRunning())
            {
                _skillProcessor.ForceFinish();
            }
            skillComponent.SetCooldown(_currentSkillID);
        }
        
        /// <summary>
        /// 检查输入是否匹配
        /// </summary>
        private bool CheckInput(string inputKey)
        {
            // 示例：检查玩家输入是否匹配
            return inputKey == "Attack" && owner.GetEntityComponent<PlayerInputComponent>().AttackInput;
        }
    }
}