using FrameWork.Component;
using FrameWork.EventCenter;
using Game.Event;
using UnityEngine;

namespace Game.Input
{
    public class PlayerInputComponent : ComponentBase
    {
        /// <summary>
        /// 方向入力
        /// </summary>
        public Vector2 DirectionlInput { get; private set; }
        /// <summary>
        /// 攻撃入力
        /// </summary>
        public bool AttackInput { get; private set; }
        /// <summary>
        /// 防御入力
        /// </summary>
        public bool DefenceInput { get; private set; }
        /// <summary>
        /// 回避入力
        /// </summary>
        public bool DashInput { get; private set; }
        /// <summary>
        /// 跳ぶ入力
        /// </summary>
        public bool JumpInput { get; private set; }
        public void OnEnable()
        {
            EventCenter.AddListener<float>(InputEvents.OnHorizontal,SetHorizontal);
            EventCenter.AddListener<float>(InputEvents.OnVertical,SetVertical);
            EventCenter.AddListener<bool>(InputEvents.OnAttack,SetAttackTrigger);
            EventCenter.AddListener<bool>(InputEvents.OnDefence,SetDefenceTrigger);
            EventCenter.AddListener<bool>(InputEvents.OnDash,SetDashTrigger);
            EventCenter.AddListener<bool>(InputEvents.OnJump,SetJumpTrigger);
        }

        public void OnDisable()
        {
            EventCenter.RemoveListener<float>(InputEvents.OnHorizontal, SetHorizontal);
            EventCenter.RemoveListener<float>(InputEvents.OnVertical, SetVertical);
            EventCenter.RemoveListener<bool>(InputEvents.OnAttack,SetAttackTrigger);
            EventCenter.RemoveListener<bool>(InputEvents.OnDefence,SetDefenceTrigger);
            EventCenter.RemoveListener<bool>(InputEvents.OnDash,SetDashTrigger);
            EventCenter.RemoveListener<bool>(InputEvents.OnJump,SetJumpTrigger);
        }

        /// <summary>
        /// 水平入力の設定
        /// </summary>
        /// <param name="inputValue">入力方向</param>
        private void SetHorizontal(float inputValue)
        {
//            Debug.Log("SetHorizontal");
            DirectionlInput= new Vector2(inputValue,DirectionlInput.y);
        }

        /// <summary>
        /// 垂直入力の設定
        /// </summary>
        /// <param name="inputValue">入力方向</param>
        private void SetVertical(float inputValue)
        {
            DirectionlInput = new Vector2(DirectionlInput.x, inputValue);
        }
    
        /// <summary>
        /// 攻撃入力トリガー
        /// </summary>
        /// <param name="isAttack">攻撃入力トリガー</param>
        private void SetAttackTrigger(bool isAttack)
        {
            AttackInput = isAttack;
        }

        /// <summary>
        /// 防御入力トリガー
        /// </summary>
        /// <param name="isDefence">パリ入力トリガー</param>
        private void SetDefenceTrigger(bool isDefence)
        {
            DefenceInput = isDefence;
        }

        /// <summary>
        /// 回避入力トリガー
        /// </summary>
        /// <param name="isDash"></param>
        private void SetDashTrigger(bool isDash)
        {
            DashInput = isDash;
        }

        /// <summary>
        /// 跳ぶ入力トリガー
        /// </summary>
        /// <param name="isJump"></param>
        private void SetJumpTrigger(bool isJump)
        {
            JumpInput = isJump;
        }
    }
}