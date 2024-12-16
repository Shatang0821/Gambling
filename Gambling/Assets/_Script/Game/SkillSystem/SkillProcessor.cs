using System.Collections.Generic;
using FrameWork.Component;
using Framework.Entity;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Game.SkillSystem
{
    public class SkillProcessor
    {
        private Skill _currentSkill;               // 現在実行中のスキル
        private List<ISkillAction> _pendingActions;     // 実行待ちのアクション
        private List<ISkillAction> _actionsToRemove;
        private float _currentTime;                // 現在のスキル経過時間
        private bool _isRunning;                   // スキルが実行中かどうか
        private EntityObject _owner;
        public SkillProcessor(EntityObject owner)
        {
            _owner = owner;
            _pendingActions = new List<ISkillAction>();
            _actionsToRemove = new List<ISkillAction>();
        }
            
        /// <summary>
        /// スキルをセットして実行を準備
        /// </summary>
        public void SetSkill(Skill skill)
        {
            _currentSkill = skill;
            _pendingActions.Clear();
            _actionsToRemove.Clear();
            _pendingActions.AddRange(skill.GetActions());
            _currentTime = 0f;
            _isRunning = true;
        }

        public void Update(float stateTime)
        {
            if(!_isRunning || _currentSkill == null || _pendingActions.Count == 0) return;
            
            _currentTime = stateTime;
            
            foreach (var action in _pendingActions)
            {
                if (action.IsActive(_currentTime))
                {
                    action.Execute(_owner);
                    if (!action.IsPersistent)
                    {
                        _actionsToRemove.Add(action);
                    }
                }
                else
                {
                    Debug.Log("実行失敗アクティブ時間以外");
                }
            }
            
            foreach (var action in _actionsToRemove)
            {
                _pendingActions.Remove(action);
            }

            if (_pendingActions.Count == 0)
            {
                FinishSkill();
            }
        }
        
        /// <summary>
        /// スキルを終了する
        /// </summary>
        private void FinishSkill()
        {
            _isRunning = false;
            Debug.Log($"Skill {_currentSkill.Name} finished.");
        }
        
        /// <summary>
        /// スキルが実行中かどうかを確認
        /// </summary>
        /// <returns>スキルが実行中の場合 true</returns>
        public bool IsRunning()
        {
            return _isRunning;
        }
        
    }
}