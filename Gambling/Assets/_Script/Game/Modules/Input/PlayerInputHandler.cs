using FrameWork.EventCenter;
using Game.Event;
using UnityEngine;

namespace Modules.Input
{
    public class PlayerInputHandler
    {
        public Vector2 DirectionlInput { get; private set; }
        
        public void OnEnable()
        {
            EventCenter.AddListener<float>(InputEvents.OnHorizontal,SetHorizontal);
            EventCenter.AddListener<float>(InputEvents.OnVertical,SetVertical);
        }

        public void OnDisable()
        {
            EventCenter.RemoveListener<float>(InputEvents.OnHorizontal, SetHorizontal);
            EventCenter.RemoveListener<float>(InputEvents.OnVertical, SetVertical);
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
    }
}