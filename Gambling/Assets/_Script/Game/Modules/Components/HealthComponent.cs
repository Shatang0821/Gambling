using FrameWork.Component;
using Framework.Entity;

namespace Game.Component
{
    public class HealthComponent : ComponentBase
    {
        // Data data Entityが持つデータ

        public void TakeDamage(float amount)
        {
            if(amount <= 0) return;
            //if(無敵) return
            
            // ダメージ処理
            // UIイベントをトリガー
            
            //HPが0以下
            //Die();
        }

        public void Heal(float amount)
        {
            if(amount <= 0)return;
        }

        public void Die()
        {
            // イベントをトリガー
        }
    }
}