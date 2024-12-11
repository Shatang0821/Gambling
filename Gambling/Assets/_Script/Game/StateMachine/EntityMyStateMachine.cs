using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;

namespace Game.StateMachine
{
    public class EntityMyStateMachine : MyStateMachine,IComponent
    {
        private EntityObject _entityObject;

        public void Initialize(EntityObject entityObject)
        {
            this._entityObject = entityObject;
            UnityEngine.Debug.Log("Initialize EntityStateMachine:  " + entityObject.name);
        }
    }
}


