using System;
using System.Collections.Generic;
using FrameWork.Component;
using Framework.Entity;
using FrameWork.Resource;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Game.Component
{
    public class HealthComponent : ComponentBase
    {
        private List<Action<float>> _damageActions = new List<Action<float>>();

        private List<Action> _deathActions = new List<Action>();
        
        private Dictionary<string, bool> _flags = new Dictionary<string, bool>();

        /// <summary>
        /// Flag を設定
        /// </summary>
        public void SetFlag(string flagName, bool value)
        {
            if (!_flags.ContainsKey(flagName))
            {
                _flags.Add(flagName, value);
            }
            else
            {
                _flags[flagName] = value;
            }
        }

        /// <summary>
        /// Flag の値を取得
        /// </summary>
        public bool GetFlag(string flagName)
        {
            return _flags.TryGetValue(flagName, out var value) && value;
        }

        /// <summary>
        /// Flag をリセット
        /// </summary>
        public void ResetFlag(string flagName)
        {
            if (_flags.ContainsKey(flagName))
            {
                _flags[flagName] = false;
            }
        }
        
        // Data data Entityが持つデータ
        public void ApplyDamage(float amount)
        {
            if (amount <= 0) return;
            
            SetFlag("TakenDamage", true);
            
            Debug.Log("Apply Damage");
            foreach (var action in _damageActions)
            {
                action.Invoke(amount);
            }

        }

        public void Die()
        {
            foreach (var action in _deathActions)
            {
                action.Invoke();
            }
        }
        
        /// <summary>
        /// ダメージ時のアクションを追加
        /// </summary>
        /// <param name="action">追加するアクション</param>
        public void AddDamageAction(Action<float> action)
        {
            if (action != null && !_damageActions.Contains(action))
            {
                _damageActions.Add(action);
            }
        }
        

        /// <summary>
        /// 死亡時のアクションを追加
        /// </summary>
        /// <param name="action">追加するアクション</param>
        public void AddDeathAction(Action action)
        {
            if (action != null && !_deathActions.Contains(action))
            {
                _deathActions.Add(action);
            }
        }
    }
}