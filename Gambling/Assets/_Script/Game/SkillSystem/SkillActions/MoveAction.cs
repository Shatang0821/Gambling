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
            var collider = owner.GetComponent<BoxCollider2D>();
            if (movement != null)
            {
                movement.Move(direction: new Vector2(owner.Direction,0),  speed: SkillActionData.Value);
                collider.enabled = false;
            }
        }

        public void Update(EntityObject owner)
        {
            Debug.Log("Test MoveAction Update");
        }

        public void Exit(EntityObject owner)
        {
            var movement = owner.GetEntityComponent<MovementComponent>();
            var collider = owner.GetComponent<BoxCollider2D>();
            if (movement != null)
            {
                movement.Stop();
                collider.enabled = true;
                //Debug.Log("停止");
            }
        }

        // public bool IsActive(float elapsedTime)
        // {
        //     return TimeStamp <= elapsedTime && elapsedTime < TimeStamp + Duration;
        // }
    }
}