using System;
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
        private List<ISkillAction> _activeActions; 
        private float _currentTime;                // 現在のスキル経過時間
        private bool _isRunning;                   // スキルが実行中かどうか
        private EntityObject _owner;
        public SkillProcessor(EntityObject owner)
        {
            _owner = owner;
            _pendingActions = new List<ISkillAction>();
            _activeActions = new List<ISkillAction>();
        }
            
        /// <summary>
        /// スキルをセットして実行を準備
        /// </summary>
        public void SetSkill(Skill skill)
        {
            _currentSkill = skill;
            _pendingActions.Clear();
            _activeActions.Clear();
            _pendingActions.AddRange(skill.GetActions());
            _pendingActions.Sort((a,b) => a.TimeStamp.CompareTo(b.TimeStamp));
            _currentTime = 0f;
            _isRunning = true;
        }

        /// <summary>
        /// 毎フレーム更新
        /// </summary>
        /// <param name="stateTime">経過時間</param>
        public void Update(float stateTime)
        {
            if(!_isRunning || _currentSkill == null) return;
            
            _currentTime = stateTime;

            // 待機中のアクションをチェックしてアクティブ化
            for (int i = _pendingActions.Count - 1; i >= 0; i--)
            {
                ISkillAction action = _pendingActions[i];

                if (_currentTime >= action.TimeStamp)
                {
                    _activeActions.Add(action); // アクティブ化
                    _pendingActions.RemoveAt(i); // 待機リストから削除
                    action.Enter(_owner); // アクション開始
                }
            }

            // アクティブアクションを更新
            for (int i = _activeActions.Count - 1; i >= 0; i--)
            {
                ISkillAction action = _activeActions[i];

                // 継続中の処理を行う
                if (action.IsPersistent)
                {
                    action.Update(_owner);
                }

                // アクションが終了した場合
                if (_currentTime >= action.TimeStamp + action.Duration)
                {
                    action.Exit(_owner);
                    _activeActions.RemoveAt(i);
                }
            }

            // 全てのアクションが終了したらスキルを終了
            if (_pendingActions.Count == 0 && _activeActions.Count == 0)
            {
                FinishSkill();
            }
        }

        /// <summary>
        /// 終了処理をリクエスト(強制終了)
        /// </summary>
        public void ForceFinish()
        {
            //Debug.Log("強制終了が呼び出された");
            // すべてのアクティブアクションを終了
            foreach (var action in _activeActions)
            {
                action.Exit(_owner);
            }
            _activeActions.Clear();

            // すべてのペンディングアクションを終了（未実行分）
            foreach (var action in _pendingActions)
            {
                action.Exit(_owner);
            }
            _pendingActions.Clear();

            // スキル終了処理
            FinishSkill();
        }
        
        /// <summary>
        /// スキルを終了する
        /// </summary>
        private void FinishSkill()
        {
            _isRunning = false;
//            Debug.Log($"Skill {_currentSkill.Name} finished.");
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