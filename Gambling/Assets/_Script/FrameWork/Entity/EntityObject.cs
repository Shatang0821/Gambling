using System;
using System.Collections.Generic;
using FrameWork.Component;
using UnityEngine;

namespace Framework.Entity
{
    public class EntityObject : MonoBehaviour
    {
        private Dictionary<Type, object> _components = new Dictionary<Type, object>();
        private IDataProvider _dataProvider;
        
        //データ提供クラスの設定
        public void SetDataProvider(IDataProvider provider)
        {
            _dataProvider = provider;
        }

        #region Component
        
        /// <summary>
        /// コンポネントを追加
        /// </summary>
        /// <param name="component"></param>
        /// <typeparam name="T"></typeparam>
        public T AddEntityComponent<T>(T component) where T : IComponent
        {
            var type = typeof(T);
            if (_components.ContainsKey(type))
            {
                Debug.LogWarning($"Component {type.Name} already exists on {name}");
                return default;
            }

            _components[type] = component;
            component.Initialize(this); // コンポネント初期化
            return component;
        }

        /// <summary>
        /// コンポネントを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetEntityComponent<T>() where T : IComponent
        {
            if (_components.TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }
            Debug.LogWarning($"Component {typeof(T).Name} not found on {name}");
            return default;
        }
        
        /// <summary>
        /// コンポネントを削除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemoveEntityComponent<T>() where T : IComponent
        {
            var type = typeof(T);
            if (_components.Remove(type))
            {
                Debug.Log($"Component {type.Name} removed from {name}");
            }
        }

        #endregion
        

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
        private void OnDestroy()
        {
            _components.Clear();
        }

        #region Transform

        /// <summary>
        /// 現在のワールド座標を取得
        /// </summary>
        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        /// <summary>
        /// ローカル座標を取得
        /// </summary>
        public Vector3 LocalPosition
        {
            get => transform.localPosition;
            set => transform.localPosition = value;
        }

        /// <summary>
        /// ワールド回転を取得
        /// </summary>
        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        /// <summary>
        /// ローカル回転を取得
        /// </summary>
        public Quaternion LocalRotation
        {
            get => transform.localRotation;
            set => transform.localRotation = value;
        }

        /// <summary>
        /// スケールを取得
        /// </summary>
        public Vector3 Scale
        {
            get => transform.localScale;
            set => transform.localScale = value;
        }

        #endregion
    }
}
