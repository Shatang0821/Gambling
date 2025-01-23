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

        public Vector2 GetVel => _rigidbody.velocity;
        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            _rigidbody = base.owner.GetComponent<Rigidbody2D>();
            if (_rigidbody == null)
            {
                Debug.LogError($"Missing Rigidbody2D on {base.owner.name}");
            }

        }

        public void SetGravity(float g)
        {
            _rigidbody.gravityScale = g;
        }
        
        public void SetVelocity(Vector2 newVel)
        {
            _rigidbody.velocity = newVel;
        }

        public void SetVelocityX(float x,bool rotation = true)
        {
            if (rotation)
            {
                if(x > 0) Rotation(1);
                if(x < 0) Rotation(-1);
            }
            _rigidbody.velocity = new Vector2(x, _rigidbody.velocity.y);
        }
        
        

        public void SetVelocityY(float y)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, y);
        }

        public void Move(Vector2 direction,float speed, bool rotation = false)
        {
            if (_rigidbody != null)
            {
                if (rotation)
                {
                    if(direction.x > 0) Rotation(1);
                    if(direction.x < 0) Rotation(-1);
                }
                var vel = new Vector2(direction.x * speed , _rigidbody.velocity.y);
                _rigidbody.velocity = vel;
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

        public void AddForce(Vector2 direction ,float Force)
        {
            if (_rigidbody != null)
            {
                _rigidbody.AddForce(direction * Force, ForceMode2D.Impulse);
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

        public void Rotation(float xDrection)
        {
            owner.transform.localScale = new Vector3(xDrection, 1, 1);
        }
    }
}