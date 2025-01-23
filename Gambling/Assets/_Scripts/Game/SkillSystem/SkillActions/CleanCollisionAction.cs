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
        public bool IsPersistent => SkillActionData.IsPersistent; // データから取得
        private int ownerLayer; // 元のレイヤーを保存
        public CleanCollisionAction(SkillActionData skillActionData)
        {
            SkillActionData = skillActionData;
        }

        public void Enter(EntityObject owner)
        {
            Debug.Log("Enter CleanCollisionAction");

            // 元のレイヤーを保存
            ownerLayer = owner.gameObject.layer;

            // ターゲットレイヤーとの当たり判定を無効化
            for (int layer = 0; layer < 32; layer++)
            {
                if ((SkillActionData.TargetSettings.TargetLayer.value & (1 << layer)) != 0)
                {
                    Physics2D.IgnoreLayerCollision(ownerLayer, layer, true);
                    //Debug.Log($"Ignoring collision between layers {ownerLayer} and {layer}");
                }
            }
        }

        public void Update(EntityObject owner)
        {
            Debug.Log("Update CleanCollisionAction");
        }

        public void Exit(EntityObject owner)
        {
            Debug.Log("Exit CleanCollisionAction");

            // ターゲットレイヤーとの当たり判定を再有効化
            for (int layer = 0; layer < 32; layer++)
            {
                if ((SkillActionData.TargetSettings.TargetLayer.value & (1 << layer)) != 0)
                {
                    Physics2D.IgnoreLayerCollision(ownerLayer, layer, false);
                    //Debug.Log($"Restoring collision between layers {ownerLayer} and {layer}");
                }
            }
        }

        // public bool IsActive(float elapsedTime)
        // {
        //     return TimeStamp <= elapsedTime && elapsedTime < TimeStamp + Duration;
        // }
    }
}