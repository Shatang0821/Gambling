using FrameWork.Component;
using Framework.Entity;
using Game.SkillSystem;
using UnityEngine;

namespace Game.Components
{
    public class DefendComponent : ComponentBase
    {
        public bool IsDefending { get; private set; }       // 現在防御状態であるか
        public bool SuccessDefend { get; private set; }
        public bool SuccessParry { get; private set; }
        private bool _canParry;
        private float _parryWindowTime;                     // パリィ可能な時間枠
        private float _blockMultiplier;                     // 防御によるダメージ軽減割合
        
        public override void Initialize(EntityObject owner)
        {
            base.Initialize(owner);
            IsDefending = false;
            _parryWindowTime = 0.3f;        // パリィの時間枠を設定
            _blockMultiplier = 0.5f;    // ダメージ軽減率（50%）
        }
        
        /// <summary>
        /// 攻撃を防御できるか試みる
        /// </summary>
        /// <param name="attackData">攻撃データ</param>
        /// <returns>防御成功かどうか</returns>
        public bool TryDefend()
        {
            // 防御状態でない場合は失敗
            if (!IsDefending) return false;

            if (_canParry)
            {
                SuccessParry = true;
                return true;
            }
            
            // 防御失敗
            return false;
        }

        /// <summary>
        /// パリできるか
        /// </summary>
        /// <param name="stateTime">防御の時間</param>
        /// <returns></returns>
        public void SetCanParry(float stateTime)
        {
            if (stateTime < _parryWindowTime)
            {
                _canParry = true;
            }
            else
            {
                _canParry = false;
            }
        }
        
        /// <summary>
        /// 防御状態を開始
        /// </summary>
        public void StartDefend()
        {
            IsDefending = true;
        }

        /// <summary>
        /// 防御状態を終了
        /// </summary>
        public void StopDefend()
        {
            IsDefending = false;
            SuccessParry = false;
        }
    }
}