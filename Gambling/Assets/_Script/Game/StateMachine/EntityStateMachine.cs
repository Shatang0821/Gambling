using FrameWork.Component;
using Framework.Entity;
using Framework.FSM;

namespace Game.StateMachine
{
    public class EntityStateMachine : MyStateMachine
    {
        protected EntityObject owner;
        /// <summary>
        /// オーナーを設定する
        /// </summary>
        /// <param name="owner">オーナー</param>
        public EntityStateMachine(EntityObject owner)
        {
            this.owner = owner;
            UnityEngine.Debug.Log("Initialize EntityStateMachine:  " + owner.name);
        }
        
    }
}


