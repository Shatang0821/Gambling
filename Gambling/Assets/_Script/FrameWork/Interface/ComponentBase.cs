using Framework.Entity;

namespace FrameWork.Component
{
    public abstract class ComponentBase
    {
        protected EntityObject entityObject;
        public abstract void Initialize(EntityObject entityObject);
    }
}