using System.Collections.Generic;
using System.Linq;
using FrameWork.Component;
using Framework.Entity;
using FrameWork.Resource;
using Game.SkillSystem;
using UnityEngine;

namespace Game.Component
{
    public class SkillComponent : ComponentBase
    {
        // スキルデータをスキル名で管理
        private Dictionary<string, SkillData> skills = new Dictionary<string, SkillData>();
        
        // 現在のクールダウンタイマー
        private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();
        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            AddSkill(ResManager.Instance.GetAssetCache<SkillData>("SkillData/Skill_1001"));

        }

        /// <summary>
        /// スキルを登録
        /// </summary>
        /// <param name="skillData">登録するスキルデータ</param>
        public void AddSkill(SkillData skillData)
        {
            if (!skills.ContainsKey(skillData.SkillName))
            {
                skills.Add(skillData.SkillName, skillData);
                cooldownTimers.Add(skillData.SkillName, 0f); // 初始化冷却时间
            }
            else
            {
                Debug.LogWarning($"Skill {skillData.SkillName} is already registered.");
            }
        }
        
        /// <summary>
        /// スキルのクールダウンチェック
        /// </summary>
        private bool IsSkillReady(string skillName)
        {
            return cooldownTimers[skillName] > 0;
        }
        
        /// <summary>
        /// スキルのクールダウン開始
        /// </summary>
        /// <param name="skillName"></param>
        public void SetCooldown(string skillName)
        {
            if (!skills.ContainsKey(skillName))
            {
                Debug.LogWarning($"Skill {skillName} does not exist.");
                return;
            }

            SkillData skillData = skills[skillName];
            cooldownTimers[skillName] = skillData.Cooldown;
        }
        
        /// <summary>
        /// スキルデータを取得する
        /// </summary>
        /// <param name="skillName">スキル名称</param>
        /// <returns>スキルデータ</returns>
        public SkillData  GetSkillData(string skillName)
        {
            if (!skills.ContainsKey(skillName))
            {
                Debug.LogWarning($"Skill {skillName} does not exist.");
                return null;
            }

            return skills[skillName];
        }

        public void UpdateCooldown()
        {   
            foreach (var skillName in cooldownTimers.Keys.ToList())
            {
                if (cooldownTimers[skillName] > 0)
                {
                    cooldownTimers[skillName] -= Time.deltaTime;
                }
            }
        }
    }
}