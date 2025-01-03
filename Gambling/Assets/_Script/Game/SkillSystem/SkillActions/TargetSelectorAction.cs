using System.Collections.Generic;
using Framework.Entity;
using Game.Component;
using Game.Utility;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class TargetSelectorAction : ISkillAction
    {
        public SkillData SkillData { get; }
        public SkillActionData SkillActionData { get; }
        public float TimeStamp { get; }
        public float Duration { get; }
        public bool IsPersistent { get; }

        private AttackComponent _attackComponent;
        public TargetSelectorAction(SkillData data,SkillActionData skillActionData)
        {
            SkillData = data;
            SkillActionData = skillActionData;
            TimeStamp = skillActionData.TimeStamp;
            Duration = skillActionData.Duration;
        }
        public void Enter(EntityObject owner)
        {
            _attackComponent = owner.GetEntityComponent<AttackComponent>();
            List<EntityObject> targets = new List<EntityObject>();
            Debug.Log("Enter TargetSelector");
            switch (SkillData.ColliderType) 
            {
                case ColliderType.Box:
                    targets = TargetSelector.DetectBox(owner.Position,SkillData.Size,new Vector2(SkillData.Offset.x * owner.Direction,SkillData.Offset.y) ,SkillData.TargetLayer);
                    break;
                case ColliderType.Circle:
                    break;
                default:
                    break;
            }
            _attackComponent.DamageFlow(targets);
            // foreach (var target in targets)
            // {
            //     Debug.Log($"Applying {SkillActionData.Value} damage to {target.name}");
            //     //target.ApplyDamage(_actionData.Value); // 
            // }
            
        }

        public void Update(EntityObject owner)
        {
            Debug.Log("aaa");
            //TargetSelector.DetectBox(owner.Position,SkillData.Size,SkillData.Offset,SkillData.TargetLayer);
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