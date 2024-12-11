using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;

namespace Game.StateMachine
{
    public class EntityStateMachine : MyStateMachine
    {
        private EntityObject _entityObject;

        public override void Initialize(EntityObject entityObject)
        {
            base.Initialize(entityObject);
            this._entityObject = entityObject;
            UnityEngine.Debug.Log("Initialize EntityStateMachine:  " + entityObject.name);
        }
    }
}


