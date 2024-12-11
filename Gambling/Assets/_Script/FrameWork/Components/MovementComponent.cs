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

        public override void Initialize(EntityObject entityObject)
        {
            this.entityObject = entityObject;
            _rigidbody = entityObject.GetComponent<Rigidbody2D>();
            if (_rigidbody == null)
            {
                Debug.LogError($"Missing Rigidbody2D on {entityObject.name}");
            }

            _speed = 5.0f; //_entity.GetAttribute("speed");
        }

        public void Move(Vector2 direction, float acceleration = -1f,bool rotation = true)
        {
            if (_rigidbody != null)
            {
                if (direction.x < 0 && rotation)
                {
                    entityObject.transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    entityObject.transform.localScale = new Vector3(1, 1, 1);
                }
                // 現在の速度
                Vector2 currentVelocity = _rigidbody.velocity;

                // 加速度が無効（-1）なら直接速度を設定
                if (acceleration < 0)
                {
                    _rigidbody.velocity = direction * _speed;
                }
                else
                {
                    // 加速度を加えた新しい速度を計算
                    Vector2 targetVelocity = direction * _speed;
                    Vector2 newVelocity = Vector2.Lerp(currentVelocity, targetVelocity, acceleration * Time.deltaTime);

                    // Rigidbodyの速度を更新
                    _rigidbody.velocity = newVelocity;
                }
            }
        }



        public void Stop()
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }
}