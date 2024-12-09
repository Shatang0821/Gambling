using FrameWork.Component;
using Framework.Entity;

public class EntityStateMachine : Framework.FSM.StateMachine,IComponent
{
    private Entity _entity;

    public void Initialize(Entity entity)
    {
        this._entity = entity;
        UnityEngine.Debug.Log("Initialize EntityStateMachine:  " + entity.name);
    }
}
