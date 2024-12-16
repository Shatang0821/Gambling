using Framework.Entity;

namespace Game.SkillSystem
{
    public interface ISkillAction
    {
        SkillActionData SkillActionData { get; }
        float StartTime { get; }                // 開始時間
        float EndTime { get; }                  // 終了時間
        bool IsPersistent { get; }              // 持続動作かどうか
        void Execute(EntityObject owner);       // 動作の実行
        bool IsActive(float elapsedTime);       // 動作が有効かどうか
    }
}