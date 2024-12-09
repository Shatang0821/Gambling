using FrameWork.Component;
using Framework.Entity;
using FrameWork.Utils;
using UnityEngine;

namespace FrameWork.Component
{
    public class MovementComponent : IComponent
    {
        private Entity _entity;
        private Rigidbody2D _rigidbody2D;
        public void Initialize(Entity entity)
        {
            this._entity = entity;
            _rigidbody2D = _entity.GetComponent<Rigidbody2D>();
            ErrorCheck.Check<Rigidbody2D>(_rigidbody2D);
        }

        public void Move(float vectorX, float acceleration, float maxSpeed, bool rotation = false)
        {
            // 加速度を計算
            Vector2 force = new Vector2(vectorX * acceleration, 0);

            // AddForceを使って力を加える
            _rigidbody2D.AddForce(force);

            // 最大速度を制限
            if (Mathf.Abs(_rigidbody2D.velocity.x) > maxSpeed)
            {
                float clampedSpeed = Mathf.Clamp(_rigidbody2D.velocity.x, -maxSpeed, maxSpeed);
                _rigidbody2D.velocity = new Vector2(clampedSpeed, _rigidbody2D.velocity.y);
            }

            // 回転の制御（オプション）
            if (rotation)
            {
                float rotationAngle = vectorX > 0 ? 0 : 180; // 移動方向に応じて回転を設定
                _entity.transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
            }
        }

        public void StopX()
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        }
    }
}