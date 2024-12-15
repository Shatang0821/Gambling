using Framework.Entity;

namespace Game.SkillSystem
{
    public interface ISkillAction
    {
        float StartTime { get; } // 
        float EndTime { get; }   // 
        void Execute(EntityObject owner);
        bool IsActive(float elapsedTime); // 
    }
}