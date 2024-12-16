using FrameWork.Component;
using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class MoveAction : ISkillAction
    {
        public SkillActionData SkillActionData { get; }
        public float StartTime { get; }
        public float EndTime { get; }
        public bool IsPersistent  => SkillActionData.IsPersistent; // データから取得

        public MoveAction(SkillActionData skillActionData)
        {
            this.SkillActionData = skillActionData;
            StartTime = skillActionData.TimeStamp;
            EndTime = skillActionData.TimeStamp + skillActionData.Duration;
        }

        public void Execute(EntityObject owner)
        {
            var movement = owner.GetEntityComponent<MovementComponent>();
            if (movement != null)
            {
                //movement.Move(direction: new Vector2(1.0f, 0.0f), speed: 10.0f);
                Debug.Log("移動中");
            }
        }

        public bool IsActive(float elapsedTime)
        {
            return StartTime <= elapsedTime && elapsedTime < EndTime;
        }
    }
}