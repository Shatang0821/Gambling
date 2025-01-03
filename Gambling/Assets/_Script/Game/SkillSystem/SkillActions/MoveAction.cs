using FrameWork.Component;
using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class MoveAction : ISkillAction
    {
        public SkillActionData SkillActionData { get; }
        public float TimeStamp { get; }
        public float Duration { get; }
        public bool IsPersistent  => SkillActionData.IsPersistent; // データから取得

        public MoveAction(SkillActionData skillActionData)
        {
            this.SkillActionData = skillActionData;
            TimeStamp = skillActionData.TimeStamp;
            Duration = skillActionData.Duration;
        }

        public void Enter(EntityObject owner)
        {
            var movement = owner.GetEntityComponent<MovementComponent>();
            if (movement != null)
            {
                movement.Move(direction: new Vector2(owner.Direction,0),  speed: SkillActionData.Value);
                Debug.Log("移動");
            }
        }

        public void Update(EntityObject owner)
        {
            Debug.Log("Test MoveAction Update");
        }

        public void Exit(EntityObject owner)
        {
            var movement = owner.GetEntityComponent<MovementComponent>();
            if (movement != null)
            {
                movement.Stop();
                Debug.Log("停止");
            }
        }

        public bool IsActive(float elapsedTime)
        {
            return TimeStamp <= elapsedTime && elapsedTime < TimeStamp + Duration;
        }
    }
}