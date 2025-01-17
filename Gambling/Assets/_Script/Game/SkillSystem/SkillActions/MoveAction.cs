using FrameWork.Component;
using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class MoveAction : ISkillAction
    {
        public SkillActionData SkillActionData { get; }
        public float TimeStamp => SkillActionData?.TimeStamp ?? 0f;
        public float Duration => SkillActionData?.Duration ?? 0f;
        public bool IsPersistent  => SkillActionData.IsPersistent; // データから取得

        public MoveAction(SkillActionData skillActionData)
        {
            SkillActionData = skillActionData;
        }

        public void Enter(EntityObject owner)
        {
            Debug.Log("Enter Move");
            var movement = owner.GetEntityComponent<MovementComponent>();
            if (movement != null)
            {
                movement.Move(direction: new Vector2(owner.FacingDir,0),  speed: SkillActionData.Value);
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
                //Debug.Log("停止");
            }
        }

        // public bool IsActive(float elapsedTime)
        // {
        //     return TimeStamp <= elapsedTime && elapsedTime < TimeStamp + Duration;
        // }
    }
}