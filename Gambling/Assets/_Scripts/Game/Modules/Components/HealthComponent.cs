using FrameWork.Component;
using Framework.Entity;
using FrameWork.Resource;
using System.Diagnostics;

namespace Game.Component
{
    public class HealthComponent : ComponentBase
    {
        // Data data Entityが持つデータ
        public void TakeDamage(float amount)
        {
            var enemyData = ResManager.Instance.GetAssetCache<EntityData>("EntityData/EnemyData");
            if (amount <= 0) return;
            //if(無敵) return

            // ダメージ処理
            enemyData.HP -= amount;
            // UIイベントをトリガー

            if (enemyData.HP <= 0)
            {
                Die();
            }
            //HPが0以下
            //Die();
        }

        public void Heal(float amount)
        {
            if (amount <= 0) return;
        }

        public void Die()
        {
            // イベントをトリガー
            UnityEngine.Debug.Log("Die");
        }
    }
}