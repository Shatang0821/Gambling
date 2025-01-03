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
        private List<ISkillAction> _pendingActions;// 実行待ちのアクション
        private ISkillAction _currentAction; 
        private float _currentTime;                // 現在のスキル経過時間
        private bool _isRunning;                   // スキルが実行中かどうか
        private EntityObject _owner;
        public SkillProcessor(EntityObject owner)
        {
            _owner = owner;
            _pendingActions = new List<ISkillAction>();
        }
            
        /// <summary>
        /// スキルをセットして実行を準備
        /// </summary>
        public void SetSkill(Skill skill)
        {
            _currentSkill = skill;
            _pendingActions.Clear();
            _pendingActions.AddRange(skill.GetActions());
            _currentAction = null;
            _currentTime = 0f;
            _isRunning = true;
        }

        public void Update(float stateTime)
        {
            if(!_isRunning || _currentSkill == null) return;
            
            _currentTime = stateTime;

            // アクションがないもしくは完成時に、次のアクションの取得を試す
            if (_currentAction == null && _pendingActions.Count > 0)
            {
                _currentAction = _pendingActions[0];
                _pendingActions.RemoveAt(0);
                _currentAction.Enter(_owner);
            }

            if (_currentAction != null)
            {
                // 更新処理があるときにさせる
                if (_currentAction.IsPersistent)
                {
                    _currentAction.Update(_owner);
                }
                // アクションの実行時間が終了した時
                if (_currentTime >= _currentAction.TimeStamp + _currentAction.Duration)
                {
                    _currentAction.Exit(_owner);
                    _currentAction = null;
                }
            }

            if (_pendingActions.Count == 0 && _currentAction == null)
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