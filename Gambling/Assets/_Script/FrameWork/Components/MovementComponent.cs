using FrameWork.Component;
using Framework.Entity;
using FrameWork.Utils;
using UnityEngine;
using Unity.VisualScripting;

namespace FrameWork.Component
{
    public class MovementComponent : ComponentBase
    {
        private Rigidbody2D _rigidbody;
        private float _speed;

        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            _rigidbody = entityObject.GetComponent<Rigidbody2D>();
            if (_rigidbody == null)
            {
                Debug.LogError($"Missing Rigidbody2D on {entityObject.name}");
            }

            _speed = 5.0f; //_entity.GetAttribute("speed");
        }

        public void Move(Vector2 direction, bool rotation)
        {
            if (_rigidbody != null)
            {
                if (rotation)
                    Rotation(direction.x);
                _rigidbody.velocity = new Vector2(direction.x * _speed , 0);
            }
                
        }

        public void Move(Vector2 direction, float acceleration,bool rotation)
        {
            if (_rigidbody != null)
            {
                if (rotation)
                    Rotation(direction.x);
                // 現在の速度
                Vector2 currentVelocity = _rigidbody.velocity;

                // 加速度を加えた新しい速度を計算
                Vector2 targetVelocity = direction * _speed;
                Vector2 newVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);

                // Rigidbodyの速度を更新
                _rigidbody.velocity = newVelocity;
                
            }
        }

        public void Move(Vector2 direction, bool rotation , float maxSpeed, float time)
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

                _rigidbody.velocity = newVelocity;


            }
        }

        public void Stop()
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }

        void Rotation(float xDrection)
        {
            entityObject.transform.localScale = new Vector3(xDrection, 1, 1);
        }

    }
}