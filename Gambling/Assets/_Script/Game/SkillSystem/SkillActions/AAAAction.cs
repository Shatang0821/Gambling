using System.Collections.Generic;
using Framework.Entity;
using Game.Utility;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class AAAAction : ISkillAction
    {
        public SkillData SkillData { get; }
        public SkillActionData SkillActionData { get; }
        public float TimeStamp { get; }
        public float Duration { get; }
        public bool IsPersistent { get; }
        
        public AAAAction(SkillData data,SkillActionData skillActionData)
        {
            SkillData = data;
            SkillActionData = skillActionData;
            TimeStamp = skillActionData.TimeStamp;
            Duration = skillActionData.Duration;
        }
        public void Enter(EntityObject owner)
        {
            List<EntityObject> targets = new List<EntityObject>();
            switch (SkillData.ColliderType) 
            {
                case ColliderType.Box:
                    targets = TargetSelector.DetectBox(owner.Position,SkillData.Size,SkillData.Offset,SkillData.TargetLayer);
                    break;
                case ColliderType.Circle:
                    break;
                default:
                    break;
            }
            
            foreach (var target in targets)
            {
                Debug.Log($"Applying {SkillActionData.Value} damage to {target.name}");
                //target.ApplyDamage(_actionData.Value); // 
            }
            
        }

        public void Update(EntityObject owner)
        {
            TargetSelector.DetectBox(owner.Position,SkillData.Size,SkillData.Offset,SkillData.TargetLayer);
        }

        public void Exit(EntityObject owner)
        {
            
        }

        public bool IsActive(float elapsedTime)
        {
            return TimeStamp <= elapsedTime && elapsedTime < TimeStamp + Duration;
        }
    }
}