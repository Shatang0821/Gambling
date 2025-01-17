using Framework.Entity;

namespace FrameWork.Component
{
    public interface IComponent
    {
        public void Initialize(EntityObject owner);
    }
}