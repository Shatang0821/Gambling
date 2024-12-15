using FrameWork.Component;
using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem
{
    public class SkillMoveAction : ISkillAction
    {
        public float StartTime { get; }
        public float EndTime { get; }

        public void Execute(EntityObject owner)
        {
            var movement = owner.GetEntityComponent<MovementComponent>();
            if (movement != null)
            {
                movement.Move(direction: new Vector2(1.0f,0.0f),speed:10.0f);
            }
        }

        public bool IsActive(float elapsedTime)
        {
            return StartTime <= elapsedTime && elapsedTime < EndTime;
        }
    }
}