using System.Collections.Generic;
using Framework.Entity;
using FrameWork.Resource;
using Game.Component;
using Game.Utility;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class SpawnEffectAction : ISkillAction
    {
        public SkillData SkillData { get; }
        public SkillActionData SkillActionData { get; }
        public float TimeStamp { get; }
        public float Duration { get; }
        public bool IsPersistent { get; }


        public SpawnEffectAction(SkillData Data, SkillActionData skillActionData)
        {
            SkillData = Data;
            SkillActionData = skillActionData;
            TimeStamp = skillActionData.TimeStamp;
            Duration = skillActionData.Duration;
            IsPersistent = skillActionData.IsPersistent;
        }

        public void Enter(EntityObject owner)
        {

        }

        public void Update(EntityObject owner)
        {
            Debug.Log("aaa");
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

