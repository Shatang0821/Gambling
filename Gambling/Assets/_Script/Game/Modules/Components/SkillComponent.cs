﻿using System.Collections.Generic;
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
        private Dictionary<int, Skill> skills = new Dictionary<int, Skill>();
        
        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            //AddSkill(ResManager.Instance.GetAssetCache<SkillData>("SkillData/Skill_1001"));

        }

        /// <summary>
        /// スキルを登録
        /// </summary>
        /// <param name="Skill">登録するスキル</param>
        public void AddSkill(Skill Skill)
        {
            if (!skills.ContainsKey(Skill.ID))
            {
                skills.Add(Skill.ID, Skill);
            }
            else
            {
                Debug.LogWarning($"Skill {Skill.Name} is already registered.");
            }
        }
        
        /// <summary>
        /// スキルのクールダウン開始
        /// </summary>
        /// <param name="skillName"></param>
        public void SetCooldown(int id)
        {
            if (!skills.ContainsKey(id))
            {
                Debug.LogWarning($"Skill {id} does not exist.");
                return;
            }
            skills[id].SetCooldown();
        }
        
        /// <summary>
        /// スキルデータを取得する
        /// </summary>
        /// <param name="id">スキルid</param>
        /// <returns>スキルデータ</returns>
        public Skill GetSkill(int id)
        {
            if (!skills.ContainsKey(id))
            {
                Debug.LogWarning($"Skill {id} does not exist.");
                return null;
            }

            return skills[id];
        }

        public void UpdateCooldown()
        {
            foreach (var skill in skills.Values)
            {
                if(!skill.IsReady())
                    skill.UpdateCooldown();
            }
        }
        
        /// <summary>
        /// スキルのクールダウンチェック
        /// </summary>
        private bool IsSkillReady(int id)
        {
            if (!skills.ContainsKey(id))
            {
                Debug.LogWarning($"Skill {id} does not exist.");
                return false;
            }

            return skills[id].IsReady();
        }
    }
}