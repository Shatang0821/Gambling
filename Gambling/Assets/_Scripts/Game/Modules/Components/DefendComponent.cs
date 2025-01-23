using FrameWork.Component;
using Framework.Entity;
using Game.SkillSystem;
using UnityEngine;

namespace Game.Components
{
    public class DefendComponent : ComponentBase
    {
        public enum DefendState
        {
            None,       // 未防御
            Blocking,   // 防御中
            Parrying,   // 弾き成功
            Invincible  // 無敵
        }
        public DefendState CurrentState { get; private set; } = DefendState.None;
        public bool SuccessParry { get; private set; }
        private float _parryWindowTime;                     // パリィ可能な時間枠
        public float BlockMultiplier { get; private set; }                     // 防御によるダメージ軽減割合
        
        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            _parryWindowTime = 0.3f;        // パリィの時間枠を設定
            BlockMultiplier = 0.5f;    // ダメージ軽減率（50%）
        }
        
        /// <summary>
        /// ダメージが受ける状態なのか
        /// </summary>
        /// <returns>ダメージが受けるがかどうか</returns>
        public bool HandleDefend()
        {
            if (CurrentState == DefendState.Parrying)
            {
                SuccessParry = true;
                return false;
            }

            return true;
        }

        public void StartInvincible()
        {
            CurrentState = DefendState.Invincible;
        }

        public void StopInvincible()
        {
            CurrentState = DefendState.None;
        }

        /// <summary>
        /// パリィ可能か設定
        /// </summary>
        public void SetParryState(float stateTime)
        {
            if (stateTime < _parryWindowTime)
            {
                CurrentState = DefendState.Parrying;
            }
            else
            {
                CurrentState = DefendState.Blocking;
            }
        }
        
        /// <summary>
        /// 防御状態を開始
        /// </summary>
        public void StartDefend()
        {
            CurrentState = DefendState.Blocking;
            SuccessParry = false;
        }

        /// <summary>
        /// 防御状態を終了
        /// </summary>
        public void StopDefend()
        {
            CurrentState = DefendState.None;
            SuccessParry = false;
        }
        
        /// <summary>
        /// 現在の防御状態をリセット
        /// </summary>
        public void ResetDefendState()
        {
            CurrentState = DefendState.None;
            SuccessParry = false;
        }
    }
}