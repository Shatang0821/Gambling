using Framework.Entity;
using Game.Components;
using UnityEngine;

namespace Game.SkillSystem.Actions
{
    public class InvincibleAction: ISkillAction
    {
        public SkillActionData SkillActionData { get; }
        public float TimeStamp => SkillActionData?.TimeStamp ?? 0f;
        public float Duration => SkillActionData?.Duration ?? 0f;
        public bool IsPersistent  => SkillActionData.IsPersistent; // データから取得

        private DefendComponent _defendComponent;
        public InvincibleAction(SkillActionData skillActionData)
        {
            SkillActionData = skillActionData;
        }
        
        public void Enter(EntityObject owner)
        {
            _defendComponent = owner.GetEntityComponent<DefendComponent>();
            Debug.Log("無敵");
            _defendComponent.StartInvincible();
        }

        
        public void Update(EntityObject owner)
        {
           
        }

        public void Exit(EntityObject owner)
        {
            Debug.Log("Stop無敵");
            _defendComponent.StopInvincible();
        }
    }
}