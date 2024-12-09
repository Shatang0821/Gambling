using FrameWork.Component;
using Framework.Entity;
using FrameWork.Utils;
using UnityEngine;

namespace FrameWork.Component
{
    public class MovementComponent : IComponent
    {
        private Entity _entity;
        private Rigidbody2D _rigidbody;
        private float _speed;

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rigidbody = _entity.GetComponent<Rigidbody2D>();
            if (_rigidbody == null)
            {
                Debug.LogError($"Missing Rigidbody2D on {_entity.name}");
            }

            _speed = 5.0f; //_entity.GetAttribute("speed");
        }

        public void Move(Vector2 direction)
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = direction * _speed;
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