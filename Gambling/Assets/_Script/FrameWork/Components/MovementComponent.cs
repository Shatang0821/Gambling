using FrameWork.Component;
using Framework.Entity;
using FrameWork.Utils;
using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

namespace FrameWork.Component
{
    public class MovementComponent : ComponentBase
    {
        private Rigidbody2D _rigidbody;

        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            _rigidbody = entityObject.GetComponent<Rigidbody2D>();
            if (_rigidbody == null)
            {
                Debug.LogError($"Missing Rigidbody2D on {entityObject.name}");
            }

        }

        public void Move(Vector2 direction,float speed, bool rotation = false)
        {
            if (_rigidbody != null)
            {
                if (rotation)
                    Rotation(direction.x);
                _rigidbody.velocity = new Vector2(direction.x * speed , 0);
            }
                
        }

        public void Move(Vector2 direction, float speed, float acceleration,bool rotation)
        {
            if (_rigidbody != null)
            {
                if (rotation)
                    Rotation(direction.x);
                // 現在の速度
                Vector2 currentVelocity = _rigidbody.velocity;

                // 加速度を加えた新しい速度を計算
                Vector2 targetVelocity = direction * speed;
                Vector2 newVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);

                // Rigidbodyの速度を更新
                _rigidbody.velocity = new Vector2(newVelocity.x,0);
                
            }
        }

        public void Move(Vector2 direction, float speed, float maxSpeed, float time, bool rotation)
        {
            if (_rigidbody != null)
            {
                if (rotation)
                    Rotation(direction.x);
                // 現在の速度
                Vector2 currentVelocity = _rigidbody.velocity;

                Vector2 targetVelocity = direction.normalized * maxSpeed;

                // 必要な加速度を計算
                float requiredAcceleration = maxSpeed / time;

                // 現在の速度からターゲット速度への補間を計算
                Vector2 newVelocity = Vector2.Lerp(currentVelocity, targetVelocity, requiredAcceleration * Time.deltaTime);

                _rigidbody.velocity = new Vector2(newVelocity.x, 0);


            }
        }

        public void Stop()
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }

        public void Stop(float decelerationRate)
        {
            if (_rigidbody != null)
            {
                // 現在の速度を取得
                Vector2 currentVelocity = _rigidbody.velocity;

                // X方向の減速処理
                float newXVelocity = Mathf.Lerp(currentVelocity.x, 1, decelerationRate * Time.deltaTime);

                // 一定の閾値以下になった場合、完全に停止
                if (Mathf.Abs(newXVelocity) < 0.01f)
                {
                    newXVelocity = 0;
                }

                // 新しい速度を適用（Xのみ更新、Yはそのまま）
                _rigidbody.velocity = new Vector2(newXVelocity, currentVelocity.y);
            }
        }

        void Rotation(float xDrection)
        {
            entityObject.transform.localScale = new Vector3(xDrection, 1, 1);
        }

        
        
    }
}