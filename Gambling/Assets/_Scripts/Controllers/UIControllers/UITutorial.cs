using FrameWork.Component;
using FrameWork.EventCenter;
using Game.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Input
{
    public class UITutorial : ComponentBase
    {
        /// <summary>
        /// •ûŒü“ü—Í
        /// </summary>
        public Vector2 DirectionlInput { get; private set; }
        /// <summary>
        /// UŒ‚“ü—Í
        /// </summary>
        public bool AttackInput { get; private set; }
        /// <summary>
        /// –hŒä“ü—Í
        /// </summary>
        public bool DefenceInput { get; private set; }
        /// <summary>
        /// ‰ñ”ğ“ü—Í
        /// </summary>
        public bool DashInput { get; private set; }
        /// <summary>
        /// ’µ‚Ô“ü—Í
        /// </summary>
        public bool JumpInput { get; private set; }
        public void OnEnable()
        {
            EventCenter.AddListener<float>(InputEvents.OnHorizontal, SetHorizontal);
            EventCenter.AddListener<float>(InputEvents.OnVertical, SetVertical);
            EventCenter.AddListener<bool>(InputEvents.OnAttack, SetAttackTrigger);
            EventCenter.AddListener<bool>(InputEvents.OnDefence, SetDefenceTrigger);
            EventCenter.AddListener<bool>(InputEvents.OnDash, SetDashTrigger);
            EventCenter.AddListener<bool>(InputEvents.OnJump, SetJumpTrigger);
        }

        public void OnDisable()
        {
            EventCenter.RemoveListener<float>(InputEvents.OnHorizontal, SetHorizontal);
            EventCenter.RemoveListener<float>(InputEvents.OnVertical, SetVertical);
            EventCenter.RemoveListener<bool>(InputEvents.OnAttack, SetAttackTrigger);
            EventCenter.RemoveListener<bool>(InputEvents.OnDefence, SetDefenceTrigger);
            EventCenter.RemoveListener<bool>(InputEvents.OnDash, SetDashTrigger);
            EventCenter.RemoveListener<bool>(InputEvents.OnJump, SetJumpTrigger);
        }

        /// <summary>
        /// …•½“ü—Í‚Ìİ’è
        /// </summary>
        /// <param name="inputValue">“ü—Í•ûŒü</param>
        private void SetHorizontal(float inputValue)
        {
            DirectionlInput = new Vector2(inputValue, DirectionlInput.y);
        }

        /// <summary>
        /// ‚’¼“ü—Í‚Ìİ’è
        /// </summary>
        /// <param name="inputValue">“ü—Í•ûŒü</param>
        private void SetVertical(float inputValue)
        {
            DirectionlInput = new Vector2(DirectionlInput.x, inputValue);
        }

        /// <summary>
        /// UŒ‚“ü—ÍƒgƒŠƒK[
        /// </summary>
        /// <param name="isAttack">UŒ‚“ü—ÍƒgƒŠƒK[</param>
        private void SetAttackTrigger(bool isAttack)
        {
            AttackInput = isAttack;
        }

        /// <summary>
        /// –hŒä“ü—ÍƒgƒŠƒK[
        /// </summary>
        /// <param name="isDefence">ƒpƒŠ“ü—ÍƒgƒŠƒK[</param>
        private void SetDefenceTrigger(bool isDefence)
        {
            DefenceInput = isDefence;
        }

        /// <summary>
        /// ‰ñ”ğ“ü—ÍƒgƒŠƒK[
        /// </summary>
        /// <param name="isDash"></param>
        private void SetDashTrigger(bool isDash)
        {
            DashInput = isDash;
        }

        /// <summary>
        /// ’µ‚Ô“ü—ÍƒgƒŠƒK[
        /// </summary>
        /// <param name="isJump"></param>
        private void SetJumpTrigger(bool isJump)
        {
            JumpInput = isJump;
        }
    }
}

