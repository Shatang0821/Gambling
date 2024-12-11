using System;

namespace Framework.FSM
{
    public class BaseState : IState
    {
        protected float stateTimer = 0;  // ステート持続時間
        private MyStateMachine _stateMachine;    //状態マシンインスタンス

        public BaseState(MyStateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
        }
        public virtual void Enter()
        {
            stateTimer = 0.0f;
        }

        public virtual void Exit()
        {
            
        }

        public virtual void LogicUpdate()
        {
            stateTimer += UnityEngine.Time.deltaTime;
        }

        public virtual void PhysicsUpdate()
        {
            
        }

        protected virtual void ChangeState<TEnum>(TEnum state)where TEnum : Enum
        {
            _stateMachine.ChangeState(state);
        }
    }
}