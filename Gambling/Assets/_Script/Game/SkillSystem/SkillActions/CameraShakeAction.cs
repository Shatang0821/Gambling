using System.Collections.Generic;
using Framework.Entity;
using FrameWork.Resource;
using Game.Component;
using Game.Utility;
using UnityEditor.XR;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class CameraShakeAction : ISkillAction
    {

        public SkillActionData SkillActionData { get; }
        public float TimeStamp { get; }
        public float Duration { get; }
        public bool IsPersistent { get; }

        public CameraShakeAction(SkillActionData skillActionData)
        {
            SkillActionData = skillActionData;
            TimeStamp = skillActionData.TimeStamp;
            Duration = skillActionData.Duration;
            IsPersistent = skillActionData.IsPersistent;
        }

        public void Enter(EntityObject owner)
        {
            CameraManager.Instance.ShakeCamera(Duration, SkillActionData.Value);
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

