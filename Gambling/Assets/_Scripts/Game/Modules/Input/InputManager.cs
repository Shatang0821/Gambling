using System;
using FrameWork.EventCenter;
using FrameWork.Utils;
using UnityEngine.InputSystem;
using Game.Event;
using UnityEngine;

namespace Game.Input
{
    public class InputManager : UnitySingleton<InputManager>,
        InputActions.IGamePlayActions,
        InputActions.ITitleActions,
        InputActions.IUIActions
    {
        private InputActions _inputActions;
        public Observer<InputDevice> CurrentDevice;
        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            _inputActions = new InputActions();
            CurrentDevice = new Observer<InputDevice>(Keyboard.current);
        }
    
        public void OnEnable()
        {
            Cursor.visible = false;                     // マウスカーソルを不可視にします。
            Cursor.lockState = CursorLockMode.Locked;   // マウスカーソルをロックする。
            
            _inputActions.GamePlay.SetCallbacks(this);
            _inputActions.Title.SetCallbacks(this);
            _inputActions.UI.SetCallbacks(this);
            
            InputSystem.onActionChange += OnActionChange;
            
        }
        
        // 無効にされた時に呼ばれるメソッド。
        private void OnDisable()
        {
            DisableAllInputs();
            
            InputSystem.onActionChange -= OnActionChange;
            
            Debug.Log("Disable");
        }
        
        // <summary>
        /// 有効actionmapを変わり
        /// </summary>
        /// <param name="actionMap">変えたいactionMap</param>
        /// <param name="isUIInput">UIの選択か</param>
        void SwitchActionMap(InputActionMap actionMap, bool isUIInput)
        {
            _inputActions.Disable();
            actionMap.Enable();

            if(isUIInput)
            {
                Cursor.visible = true;                     // マウスカーソルを可視にします。
                Cursor.lockState = CursorLockMode.None;    // マウスカーソルをロックしない。
            }
            else
            {
                Cursor.visible = false;                     // マウスカーソルを不可視にします。
                Cursor.lockState = CursorLockMode.Locked;   // マウスカーソルをロックする。
            }
        }
        
        /// <summary>
        /// ゲーム内でキャラクターを操作する時に入力を有効化するメソッド。
        /// </summary>
        public void EnableGameplayInput() => SwitchActionMap(_inputActions.GamePlay, false);

        /// <summary>
        /// 一時停止画面内の入力を有効化するメソッド
        /// </summary>
        public void EnableTitleInput() => SwitchActionMap(_inputActions.Title, false);
        
        /// <summary>
        /// 入力を無効化する
        /// </summary>
        public void DisableAllInputs()　=> _inputActions.Disable();
    
        /// <summary>
        /// デバイス切り替え処理
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="actionChange"></param>
        private void OnActionChange(object obj, InputActionChange actionChange)
        {
            if (actionChange == InputActionChange.ActionStarted)
            {
                var d = ((InputAction)obj).activeControl.device;
                switch (d.device)
                {
                    case Keyboard:
                        if (CurrentDevice.Value == Keyboard.current)
                            return;
                        CurrentDevice.Value = Keyboard.current;
                        //Debug.Log(_currentDevice);
                        break;
                    case Gamepad:
                        if (CurrentDevice.Value == Gamepad.current)
                            return;
                        CurrentDevice.Value = Gamepad.current;
                        //Debug.Log(_currentDevice);
                        break;
                }
            }
        }

        #region InputAction

        public void OnHorizontal(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                EventCenter.TriggerEvent<float>(InputEvents.OnHorizontal,context.ReadValue<float>());
            }

            if (context.canceled)
            {
                EventCenter.TriggerEvent<float>(InputEvents.OnHorizontal,0.0f);
            }
        }

        public void OnVertical(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                EventCenter.TriggerEvent<float>(InputEvents.OnVertical,context.ReadValue<float>());
            }

            if (context.canceled)
            {
                EventCenter.TriggerEvent<float>(InputEvents.OnVertical,0.0f);
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                EventCenter.TriggerEvent<bool>(InputEvents.OnAttack,true);
            }

            if (context.canceled)
            {
                EventCenter.TriggerEvent<bool>(InputEvents.OnAttack,false);
            }
        }

        public void OnDefence(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                EventCenter.TriggerEvent<bool>(InputEvents.OnDefence,true);
            }

            if (context.canceled)
            {
                EventCenter.TriggerEvent<bool>(InputEvents.OnDefence,false);
            }
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                EventCenter.TriggerEvent<bool>(InputEvents.OnDash,true);
            }

            if (context.canceled)
            {
                EventCenter.TriggerEvent<bool>(InputEvents.OnDash,false);
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                EventCenter.TriggerEvent<bool>(InputEvents.OnJump,true);
            }

            if (context.canceled)
            {
                EventCenter.TriggerEvent<bool>(InputEvents.OnJump,false);
            }
        }

        public void OnReturn(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                EventCenter.TriggerEvent(TitleEvents.OnClosePanel);
            }
        }

        #endregion

        
        
    }
}

