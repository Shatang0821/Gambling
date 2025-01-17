using Framework.Entity;

namespace FrameWork.Component
{
    public class ComponentBase : IComponent
    {
        protected EntityObject owner;
        public virtual void Initialize(EntityObject owner)
        {
            this.owner = owner;
        }
    }
}