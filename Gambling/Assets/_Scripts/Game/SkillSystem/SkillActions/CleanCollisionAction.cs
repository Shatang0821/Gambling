using FrameWork.Component;
using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class CleanCollisionAction : ISkillAction
    {
        public SkillActionData SkillActionData { get; }
        public float TimeStamp => SkillActionData?.TimeStamp ?? 0f;
        public float Duration => SkillActionData?.Duration ?? 0f;
        public bool IsPersistent => SkillActionData.IsPersistent; // ÉfÅ[É^Ç©ÇÁéÊìæ

        public CleanCollisionAction(SkillActionData skillActionData)
        {
            SkillActionData = skillActionData;
        }

        public void Enter(EntityObject owner)
        {
            Debug.Log("Enter Move");
            var collision = owner.GetComponent<BoxCollider2D>();
            collision.enabled = false;
        }

        public void Update(EntityObject owner)
        {
            Debug.Log("Test MoveAction Update");
        }

        public void Exit(EntityObject owner)
        {
            var collision = owner.GetComponent<BoxCollider2D>();
            collision.enabled = true;
        }

        // public bool IsActive(float elapsedTime)
        // {
        //     return TimeStamp <= elapsedTime && elapsedTime < TimeStamp + Duration;
        // }
    }
}