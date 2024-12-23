using System;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Component;
using Framework.Entity;

namespace Game.Component
{
    public class AttackComponent : ComponentBase
    {
        private MovementComponent _movementComponent;
        private TargetSelectorComponent _targetSelectorComponent;

        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            _targetSelectorComponent = owner.GetEntityComponent<TargetSelectorComponent>();
        }


        public void DamageFlow()
        {
            // ターゲットの検出
            EntityObject target = _targetSelectorComponent.OverlapAttack(entityObject.LocalPosition,new Vector2(1.5f, 2),new Vector2(1.5f, 0.5f));

            //EntityObject closerangetarget = _targetSelectorComponent.CloseRangeAttack(entityObject.LocalPosition,45,3,10);
            
            if (target != null)
            {
                // MovementComponent の取得
                _movementComponent = target.GetEntityComponent<MovementComponent>();
                //_movementComponent = closerangetarget.GetEntityComponent<MovementComponent>();

                if (_movementComponent != null)
                {
                    // 攻撃者からターゲットへの方向を計算
                    Vector2 direction = (target.LocalPosition - entityObject.LocalPosition).normalized;
                    //Vector2 direction = (closerangetarget.LocalPosition - entityObject.LocalPosition).normalized;

                    // 移動させる
                    _movementComponent.AddForce(direction, 2.0f);

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
            else
            {
                Debug.Log("No target detected.");
            }
        }

    }
}