using System;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Component;
using Framework.Entity;

namespace Game.Component
{
    public class AttackComponent : ComponentBase
    {
        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
        }

        public void CloseRangeAttack(Vector2 origin, float angleRange, float maxDistance, int rayCount)
        {
            // 計算された角度の間隔
            float angleStep = angleRange / (rayCount - 1);

            // レイの中央角度を基準とする
            float startAngle = -angleRange / 2;

            for (int i = 0; i < rayCount; i++)
            {
                // 現在のレイの角度を計算
                float currentAngle = startAngle + i * angleStep;

                // 角度をベクトルに変換
                Vector2 direction = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));

                // レイを飛ばす
                RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance);

                // ヒット判定
                if (hit.collider != null)
                {
                    // ヒットした対象に対して何らかの処理を実行
                    Debug.Log($"Hit: {hit.collider.name}");

                }

                // レイの可視化（デバッグ用）
                Debug.DrawRay(origin, direction * maxDistance, Color.red, 1f);
            }
        }
    }
}
