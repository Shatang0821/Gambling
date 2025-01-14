using System.Collections.Generic;
using Framework.Entity;
using Game.Component;
using Game.Utility;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class TargetSelectorAction : ISkillAction
    {
        public SkillActionData SkillActionData { get; }
        public float TimeStamp { get; }
        public float Duration { get; }
        public bool IsPersistent { get; }

        private AttackComponent _attackComponent;
        public TargetSelectorAction(SkillActionData skillActionData)
        {
            SkillActionData = skillActionData;
            TimeStamp = skillActionData.TimeStamp;
            Duration = skillActionData.Duration;
        }
        public void Enter(EntityObject owner)
        {
            _attackComponent = owner.GetEntityComponent<AttackComponent>();
            List<EntityObject> targets = new List<EntityObject>();
            Debug.Log("Enter TargetSelector");
            var targetSettings = SkillActionData.TargetSettings;
            switch (targetSettings.ColliderType) 
            {
                case ColliderType.Box:
                    targets = TargetSelector.DetectBox(owner.Position, targetSettings.Size,
                        new Vector2(targetSettings.Offset.x * owner.Direction, targetSettings.Offset.y),
                        targetSettings.TargetLayer);
                    break;
                case ColliderType.Circle:
                    targets = TargetSelector.DetectCircle(owner.Position + targetSettings.Offset, targetSettings.Radius,
                        targetSettings.TargetLayer);
                    break;
                default:
                    break;
            }
            _attackComponent.DamageFlow(targets);
        }

        public void Update(EntityObject owner)
        {
            
        }

        public void Exit(EntityObject owner)
        {
            
        }

        //public bool IsActive(float elapsedTime) => TimeStamp <= elapsedTime && elapsedTime < TimeStamp + Duration;
    }
}