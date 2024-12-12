
using System;
using Framework.Entity;
using FrameWork.EventCenter;
using UnityEngine.InputSystem;
using Game.Event;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Input
{
    public class InputManager : FrameWork.Utils.Singleton<InputManager>,
        InputActions.IGamePlayActions
    {
        private InputActions _inputActions;

        public void Initialize()
        {
            _inputActions = new InputActions();
        }
    
        public void OnEnable()
        {
            _inputActions.GamePlay.SetCallbacks(this);
            _inputActions.GamePlay.Enable();
        }

        public void OnDisable()
        {
            _inputActions.Disable();
        }

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
    }
}

