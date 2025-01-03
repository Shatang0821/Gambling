using System;
using System.Collections.Generic;
using UnityEngine;
using FrameWork.Component;
using Framework.Entity;

namespace Game.Utility
{
    public class TargetSelector
    {
        /// <summary>
        /// 円形範囲内のターゲットを検出
        /// </summary>
        /// <param name="origin">検出範囲の中心点</param>
        /// <param name="radius">円の半径</param>
        /// <param name="layerMask">対象とするレイヤーマスク</param>
        /// <returns>検出されたターゲットのリスト</returns>
        public static List<EntityObject> DetectCircle(Vector2 origin, float radius, LayerMask layerMask)
        {
            List<EntityObject> targets = new List<EntityObject>();

            // 円形範囲内にある全てのコライダーを取得
            Collider2D[] colliders = Physics2D.OverlapCircleAll(origin, radius, layerMask);

            foreach (var collider in colliders)
            {
                // コライダーに紐づけられた EntityObject を取得
                var entity = collider.GetComponent<EntityObject>();
                if (entity != null && !targets.Contains(entity))
                {
                    targets.Add(entity);
                }
            }

            // 検出範囲をデバッグ描画
            DebugDrawCircle(origin, radius);

            return targets;
        }

        /// <summary>
        /// 矩形範囲内のターゲットを検出
        /// </summary>
        /// <param name="origin">矩形の中心点</param>
        /// <param name="size">矩形の幅と高さ</param>
        /// <param name="offset">中心点からのオフセット</param>
        /// <param name="layerMask">対象とするレイヤーマスク</param>
        /// <returns>検出されたターゲットのリスト</returns>
        public static List<EntityObject> DetectBox(Vector2 origin, Vector2 size, Vector2 offset, LayerMask layerMask)
        {
            List<EntityObject> targets = new List<EntityObject>();

            // 矩形範囲内にある全てのコライダーを取得
            Collider2D[] colliders = Physics2D.OverlapBoxAll(origin + offset, size, 0f, layerMask);

            foreach (var collider in colliders)
            {
                // コライダーに紐づけられた EntityObject を取得
                var entity = collider.GetComponent<EntityObject>();
                if (entity != null && !targets.Contains(entity))
                {
                    targets.Add(entity);
                }
            }

            // 検出範囲をデバッグ描画
            DebugDrawRectangle(origin + offset, size);

            return targets;
        }

        /// <summary>
        /// 円形範囲をデバッグ描画
        /// </summary>
        /// <param name="origin">円の中心点</param>
        /// <param name="radius">円の半径</param>
        /// <param name="segments">円を構成する分割数</param>
        private static void DebugDrawCircle(Vector2 origin, float radius, int segments = 32)
        {
            float angleStep = 360f / segments; // 円を分割する角度のステップ
            Vector2 previousPoint = origin + Vector2.right * radius; // 最初の点

            for (int i = 1; i <= segments; i++)
            {
                float angle = angleStep * i * Mathf.Deg2Rad;
                Vector2 newPoint = origin + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

                Debug.DrawLine(previousPoint, newPoint, Color.green, 1f);
                previousPoint = newPoint; // 次の点に移動
            }
        }

        /// <summary>
        /// 矩形範囲をデバッグ描画
        /// </summary>
        /// <param name="origin">矩形の中心点</param>
        /// <param name="size">矩形の幅と高さ</param>
        private static void DebugDrawRectangle(Vector2 origin, Vector2 size)
        {
            // 矩形の四隅を計算
            Vector2 topLeft = origin + new Vector2(-size.x / 2, size.y / 2);
            Vector2 topRight = origin + new Vector2(size.x / 2, size.y / 2);
            Vector2 bottomLeft = origin + new Vector2(-size.x / 2, -size.y / 2);
            Vector2 bottomRight = origin + new Vector2(size.x / 2, -size.y / 2);

            // 四辺をデバッグ描画
            Debug.DrawLine(topLeft, topRight, Color.green, 1f);
            Debug.DrawLine(topRight, bottomRight, Color.green, 1f);
            Debug.DrawLine(bottomRight, bottomLeft, Color.green, 1f);
            Debug.DrawLine(bottomLeft, topLeft, Color.green, 1f);
        }

    }
}
