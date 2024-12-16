using System.Collections.Generic;
using System.Linq;
using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem
{
    public class Skill
    {
        private SkillData _skillData;
        public readonly List<ISkillAction> _actions;
        private readonly EntityObject _owner;
        private float _cooldownTimer = 0f;
        
        public string Name => _skillData.SkillName;
        public int ID => _skillData.SkillID;
        public string AnimationName => _skillData.AnimationName;

        public Skill( EntityObject owner,SkillData skillData,List<ISkillAction> actions)
        {
            _owner = owner;
            _skillData = skillData;
            _actions = actions;
        }
        
        /// <summary>
        /// アクションのリストを取得
        /// </summary>
        public List<ISkillAction> GetActions()
        {
            return _actions;
        }
        
        /// <summary>
        /// スキルデータを取得
        /// </summary>
        public SkillData GetSkillData()
        {
            return _skillData;
        }

        /// <summary>
        /// クールダウンの更新
        /// </summary>
        public void UpdateCooldown()
        {
            if (_cooldownTimer > 0)
            {
                _cooldownTimer -= Time.deltaTime;
                _cooldownTimer = Mathf.Max(0, _cooldownTimer);
            }
        }

        /// <summary>
        /// 準備できたが
        /// </summary>
        /// <returns></returns>
        public bool IsReady()
        {
            return _cooldownTimer <= 0;
        }

        public void SetCooldown()
        {
            _cooldownTimer = _skillData.Cooldown;
        }

        public void SetCooldown(float time)
        {
            if (time >= 0 && time <= _skillData.Cooldown)
            {
                _cooldownTimer = time;   
            }
        }
    }
}