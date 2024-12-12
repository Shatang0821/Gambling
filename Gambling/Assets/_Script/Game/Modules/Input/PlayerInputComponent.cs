using FrameWork.Component;
using FrameWork.EventCenter;
using Game.Event;
using UnityEngine;

namespace Game.Input
{
    public class PlayerInputComponent : ComponentBase
    {
        public Vector2 DirectionlInput { get; private set; }
        public bool AttackInput { get; private set; }
        public bool DefenceInput { get; private set; }
        public void OnEnable()
        {
            EventCenter.AddListener<float>(InputEvents.OnHorizontal,SetHorizontal);
            EventCenter.AddListener<float>(InputEvents.OnVertical,SetVertical);
            EventCenter.AddListener<bool>(InputEvents.OnAttack,SetAttackTrigger);
            EventCenter.AddListener<bool>(InputEvents.OnDefence,SetDefenceTrigger);
        }

        public void OnDisable()
        {
            EventCenter.RemoveListener<float>(InputEvents.OnHorizontal, SetHorizontal);
            EventCenter.RemoveListener<float>(InputEvents.OnVertical, SetVertical);
            EventCenter.RemoveListener<bool>(InputEvents.OnAttack,SetAttackTrigger);
            EventCenter.RemoveListener<bool>(InputEvents.OnDefence,SetDefenceTrigger);
        }

        /// <summary>
        /// 水平入力の設定
        /// </summary>
        /// <param name="inputValue">入力方向</param>
        private void SetHorizontal(float inputValue)
        {
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
        /// 攻撃入力があるか
        /// </summary>
        /// <param name="isAttack">攻撃入力トリガー</param>
        private void SetAttackTrigger(bool isAttack)
        {
            AttackInput = isAttack;
        }

        /// <summary>
        /// パリ入力があるか
        /// </summary>
        /// <param name="isDefence">パリ入力トリガー</param>
        private void SetDefenceTrigger(bool isDefence)
        {
            DefenceInput = isDefence;
        }
    }
}