using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;
using UnityEngine;

public class EntityStateMachine : Framework.FSM.StateMachine,IComponent
{
    private Entity _entity;

    public void Initialize(Entity entity)
    {
        this._entity = entity;
        Debug.Log("Initialize EntityStateMachine:  " + entity.name);
    }
}