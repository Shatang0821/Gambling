using System.Collections.Generic;
using System.Linq;
using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem
{
    public class Skill
    {
        public string Name => _skillData.SkillName;
        public int ID => _skillData.SkillID;

        private readonly List<ISkillAction> _actions;
        private readonly EntityObject _owner;
        private float _cooldownTimer = 0f;
        private SkillData _skillData;
        public Skill( EntityObject owner,SkillData skillData)
        {
            _owner = owner;
            _skillData = skillData;
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