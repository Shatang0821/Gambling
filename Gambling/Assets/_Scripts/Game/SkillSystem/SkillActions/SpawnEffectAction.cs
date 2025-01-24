using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class SpawnEffectAction : ISkillAction
    {

        public SkillActionData SkillActionData { get; }
        public float TimeStamp { get; }
        public float Duration { get; }
        public bool IsPersistent { get; }
        public GameObject SpawnEffect {  get; }

        public SpawnEffectAction(SkillActionData skillActionData)
        {
            SkillActionData = skillActionData;
            TimeStamp = skillActionData.TimeStamp;
            Duration = skillActionData.Duration;
            IsPersistent = skillActionData.IsPersistent;
            SpawnEffect = skillActionData.Prefab;
        }

        public void Enter(EntityObject owner)
        {
//            Debug.Log("Enter SpawnEffect");
//            Debug.Log(owner.LocalPosition);
            EffectManager.Instance.SpawnEffect(SpawnEffect, owner.LocalPosition, Quaternion.identity);
        }

        public void Update(EntityObject owner)
        {

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

