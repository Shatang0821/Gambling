using System;
using System.Collections;
using System.Collections.Generic;
using FrameWork.Component;
using FrameWork.Components;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Framework.Entity
{
    public class Entity : MonoBehaviour
    {
        private Dictionary<Type, object> _components = new Dictionary<Type, object>();
        private IDataProvider _dataProvider;

        //データ提供クラスの設定
        public void SetDataProvider(IDataProvider provider)
        {
            _dataProvider = provider;
        }

        // コンポネントを追加
        public void AddEntityComponent<T>(T component) where T : IComponent
        {
            var type = typeof(T);
            if (_components.ContainsKey(type))
            {
                Debug.LogWarning($"Component {type.Name} already exists on {name}");
                return;
            }

            _components[type] = component;
            component.Initialize(this); // コンポネント初期化
        }

        // コンポネントを取得
        public T GetEntityComponent<T>() where T : IComponent
        {
            if (_components.TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }
            Debug.LogWarning($"Component {typeof(T).Name} not found on {name}");
            return default;
        }

        // コンポネントを削除
        public void RemoveEntityComponent<T>() where T : IComponent
        {
            var type = typeof(T);
            if (_components.Remove(type))
            {
                Debug.Log($"Component {type.Name} removed from {name}");
            }
        }

        // データ取得
        public float GetAttribute(string key)
        {
            return _dataProvider?.GetData(key) ?? 0.0f;
        }

        // ライフサイクル：初期化
        public virtual void Initialize()
        {
            Debug.Log($"{name} initialized.");
        }
        
        // ライフサイクル：削除
        public virtual void DestroyEntity()
        {
            Debug.Log($"{name} destroyed.");
            _components.Clear();
        }
    }
}
