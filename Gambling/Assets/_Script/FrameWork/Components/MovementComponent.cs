using FrameWork.Component;
using Framework.Entity;
using FrameWork.Utils;
using UnityEngine;

namespace FrameWork.Component
{
    public class MovementComponent : IComponent
    {
        private EntityObject _entityObject;
        private Rigidbody2D _rigidbody;
        private float _speed;

        public void Initialize(EntityObject entityObject)
        {
            _entityObject = entityObject;
            _rigidbody = _entityObject.GetComponent<Rigidbody2D>();
            if (_rigidbody == null)
            {
                Debug.LogError($"Missing Rigidbody2D on {_entityObject.name}");
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