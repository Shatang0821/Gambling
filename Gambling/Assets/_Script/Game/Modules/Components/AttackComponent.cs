using System;
using System.Collections.Generic;
using Framework.Aduio;
using UnityEngine;
using FrameWork.Component;
using Framework.Entity;
using FrameWork.Resource;
using Unity.Mathematics;

namespace Game.Component
{
    public class AttackComponent : ComponentBase
    {
        private MovementComponent _movementComponent;

        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            //_targetSelector = owner.GetEntityComponent<TargetSelector>();
        }


        public void DamageFlow(List<EntityObject> targets)
        {
            //EntityObject closerangetarget = _targetSelectorComponent.CloseRangeAttack(entityObject.LocalPosition,45,3,10);
            
            if (targets != null && targets.Count != 0)
            {
                foreach (var target in targets)
                {
                    // MovementComponent の取得
                    _movementComponent = target.GetEntityComponent<MovementComponent>();
                    //_movementComponent = closerangetarget.GetEntityComponent<MovementComponent>();
            
                    if (_movementComponent != null)
                    {
                        // 攻撃者からターゲットへの方向を計算
                        Vector2 direction = (target.LocalPosition - entityObject.LocalPosition).normalized;
                        //Vector2 direction = (closerangetarget.LocalPosition - entityObject.LocalPosition).normalized;
                        EffectManager.Instance.SpawnEffect(
                            ResManager.Instance.GetAssetCache<GameObject>("Prefabs/Effects/Hit_01"),target.Position,quaternion.identity);
                        EffectManager.Instance.SpawnEffect(
                            ResManager.Instance.GetAssetCache<GameObject>("Prefabs/Effects/Break_01"),target.Position,quaternion.identity);
                        CameraManager.Instance.ShakeCamera(0.1f,0.08f);
                        TimeManager.Instance.PauseTime(0.05f);
                        // 移動させる
                        _movementComponent.AddForce(direction, 0.8f);
            
                        // デバッグログを出力
                        Debug.Log($"Target: {target.name}, Position: {target.transform.position}, Direction: {direction}");
                        //Debug.Log($"Target: {closerangetarget.name}, Position: {closerangetarget.transform.position}, Direction: {direction}");
                    }
                    else
                    {
                        Debug.LogError($"Target {target.name} does not have a MovementComponent.");
                        //Debug.LogError($"Target {closerangetarget.name} does not have a MovementComponent.");
                    }
                }
                
            }
            else
            {
                Debug.Log("No target detected.");
            }
        }

    }
}