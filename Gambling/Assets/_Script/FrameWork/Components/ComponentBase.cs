using Framework.Entity;

namespace FrameWork.Component
{
    public class ComponentBase : IComponent
    {
        protected EntityObject entityObject;
        public virtual void Initialize(EntityObject owner)
        {
            entityObject = owner;
        }
    }
}