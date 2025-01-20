using System;
using System.Collections.Generic;
using Framework.Aduio;
using UnityEngine;
using FrameWork.Component;
using Framework.Entity;
using FrameWork.Resource;
using Game.Components;
using Game.SkillSystem;
using Unity.Mathematics;
using Unity.VisualScripting;

namespace Game.Component
{
    public class AttackComponent : ComponentBase
    {
        
        /// <summary>
        /// ダメージを与える
        /// </summary>
        /// <param name="targets">与えるターゲット</param>
        /// <param name="fbData">フィードバックデータ</param>
        public void DamageFlow(List<EntityObject> targets, FeedBackData fbData)
        {
            if (targets != null && targets.Count != 0)
            {
                foreach (var target in targets)
                {
                    if (!TryDefend(target))
                    {
                        ExecuteFeedback(target, fbData);
                        ApplyDamage(target, fbData);
                    }
                        
                }
            }
            else
            {
                Debug.Log("No target detected.");
            }
        }

        /// <summary>
        /// 防御処理を試みる
        /// </summary>
        /// <param name="target">ターゲット</param>
        /// <param name="feedBackData">フィードバックデータ</param>
        /// <returns></returns>
        public bool TryDefend(EntityObject target)
        {
            var defendComponent = target.GetEntityComponent<DefendComponent>();
            if (defendComponent != null && defendComponent.TryDefend())
            {
                Debug.Log($"{target.name} は攻撃を防御しました。");
                return true;
            }

            return false;
        }

        public void ApplyDamage(EntityObject target,FeedBackData feedBackData)
        {
            // デバッグログを出力
            Debug.Log($"Target: {target.name}, にダメージを与える");
            var healthComponent = target.GetEntityComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.ApplyDamage(feedBackData.Damage);
            }
        }

        /// <summary>
        /// フィードバックデータの実行
        /// </summary>
        private void ExecuteFeedback(EntityObject target, FeedBackData feedback)
        {
            // デバッグログを出力
            Debug.Log($"Target: {target.name}, Position: {target.transform.position}");

            if (feedback == null) return;

            // カメラシェーク
            if (feedback.EnableCameraShake)
            {
                CameraManager.Instance.ShakeCamera(feedback.ShakeDuration,feedback.ShakeIntensity);
            }

            // フレームフリーズ
            if (feedback.EnableTimeFreeze)
            {
                TimeManager.Instance.PauseTime(feedback.FreezeDuration,feedback.FreezeScale);
            }

            // エフェクト生成
            if (feedback.EnableEffect && feedback.EffectPrefabs.Length != 0)
            {
                foreach (var prefab in feedback.EffectPrefabs)
                {
                    if (prefab != null)
                    {
                        Vector3 position = owner.Position + new Vector3(feedback.EffectOffset.x * owner.FacingDir, feedback.EffectOffset.y);
                        EffectManager.Instance.SpawnEffect(prefab, position, quaternion.identity);
                    }
                }
            }

            // 音声再生
            if (feedback.EnableAudio && feedback.AudioData != null)
            {
                AudioManager.Instance.PlaySFX(feedback.AudioData);
            }

            // ノックバック処理
            if (feedback.EnableKnockback)
            {
                var movementComponent = target.GetEntityComponent<MovementComponent>();
                if (movementComponent != null)
                {
                    // 攻撃者からターゲットへの方向を計算
                    Vector2 direction = (target.LocalPosition - owner.LocalPosition).normalized;
                    movementComponent.AddForce(direction, feedback.KnockbackForce);
                }
                else
                {
                    Debug.Log("MoveComponentが存在しない");
                }
            }
        }
    }
}