namespace Framework.FSM
{
    public interface IState
    {
        public bool CanBeInterrupted { get; }
        public void Enter();
        public void Exit();
        public void LogicUpdate();
        public void PhysicsUpdate();
    }
}