using Framework.Entity;

namespace Game.SkillSystem
{
    public interface ISkillAction
    {
        SkillActionData SkillActionData { get; }
        float TimeStamp { get; }                // タイムスタンプ
        float Duration  { get ; }                // 継続時間
        bool IsPersistent { get; }              // 持続動作かどうか
        void Enter(EntityObject owner);         // 
        void Update(EntityObject owner);       　// 動作の更新処理
        void Exit(EntityObject owner);
        //bool IsActive(float elapsedTime);       // 動作が有効かどうか
    }
}