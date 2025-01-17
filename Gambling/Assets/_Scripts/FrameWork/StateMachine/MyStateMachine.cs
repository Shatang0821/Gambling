using Framework.Entity;
using FrameWork.Component;
using System;
using System.Collections.Generic;

namespace Framework.FSM
{
    public class MyStateMachine
    {
        protected IState currentState;

        private Dictionary<string, IState> _stateTable = new();

        /// <summary>
        /// ステートの初期設定
        /// </summary>
        /// <param name="startState">設定したい状態</param>
        public void InitState(Enum startState)
        {
            ChangeState(startState);
        }

        public void ChangeState(Enum newState)
        {
            var stateName = newState.ToString();
            if (_stateTable.TryGetValue(stateName, out IState state))
            {
                currentState?.Exit();
                currentState = state;
                currentState.Enter();
            }
        }
        
        public void LogicUpdate()
        {
            currentState?.LogicUpdate();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }

        public void RegisterState(Enum stateEnum, IState state)
        {
            _stateTable[stateEnum.ToString()] = state;
        }


    }
}
