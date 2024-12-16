using System;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Component;
using Framework.Entity;

namespace Game.Component
{
    public class TargetSelectorComponent : ComponentBase
    {
        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
        }
        #region 近距離
        /// <summary>
        /// 近距離攻撃
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="angleRange"></param>
        /// <param name="maxDistance"></param>
        /// <param name="rayCount"></param>
        public EntityObject CloseRangeAttack(Vector2 origin, float angleRange, float maxDistance, int rayCount)
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
                if (hit.collider != null)
                {
                    Debug.Log($"Hit: {hit.collider.name}");
                    var entity = hit.collider.GetComponent<EntityObject>();
                    if (entity != null)
                    {
                        Debug.Log("a");
                        return entity;
                    }

                }
                // レイの可視化（デバッグ用）
                Debug.DrawRay(origin, direction * maxDistance, Color.red, 1f);
            }
            return null;
        }
        #endregion

        #region overlap
        /// <summary>
        /// overlapによる判定
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        public EntityObject OverlapAttack(Vector2 origin, Vector2 size ,Vector2 offset)
        {
            DrawDebugRectangle(origin + offset, size);
            // 四角形範囲で重なっているコライダーを取得
            Collider2D[] colliders = Physics2D.OverlapBoxAll(origin + offset, size, 0f); // 0f は回転角度

            foreach (var collider in colliders)
            {
                // ここでターゲットに対する処理を行う
                Debug.Log($"Hit: {collider.name}");
                if (collider.CompareTag("Player")) 
                {
                    var entity = collider.GetComponent<EntityObject>();
                    if (entity != null)
                    {
                        Debug.Log(entity);
                        return entity;
                    }
                }
                
            }
            return null;
        }

        //四角のデバック表示
        private void DrawDebugRectangle(Vector2 origin, Vector2 size)
        {
            // 四角形のコーナー位置を計算
            Vector2 topLeft = origin + new Vector2(-size.x / 2, size.y / 2);
            Vector2 topRight = origin + new Vector2(size.x / 2, size.y / 2);
            Vector2 bottomLeft = origin + new Vector2(-size.x / 2, -size.y / 2);
            Vector2 bottomRight = origin + new Vector2(size.x / 2, -size.y / 2);

            // 四角形の辺を繋げてデバッグ表示
            Debug.DrawLine(topLeft, topRight, Color.red);
            Debug.DrawLine(topRight, bottomRight, Color.red);
            Debug.DrawLine(bottomRight, bottomLeft, Color.red);
            Debug.DrawLine(bottomLeft, topLeft, Color.red);
        }
        #endregion

    }
}
