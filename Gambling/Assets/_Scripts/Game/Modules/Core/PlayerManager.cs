using System;
using FrameWork.EventCenter;
using Game.Component;
using Game.Entity;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game.Core
{

    public class PlayerManager : MonoBehaviour
    {
        public GameObject Player;
        private Player SpawnedPlayer;
        public Vector3 SpawnPos;
        
        /// <summary>
        /// プレイヤを生成
        /// </summary>
        public void SpawnPlayer()
        {
            if (SpawnedPlayer != null)
            {
                DestroyPlayer();
            }
            SpawnedPlayer = Instantiate(Player, SpawnPos, quaternion.identity).GetComponent<Player>();
            var healthComponent = SpawnedPlayer.GetEntityComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.AddDeathAction(WhenPlayDeath);
            }
        }

        public void DestroyPlayer()
        {
            if(SpawnedPlayer == null) return;
            Destroy(SpawnedPlayer.gameObject);
        }

        /// <summary>
        /// ゲームオーバー
        /// </summary>
        public void WhenPlayDeath()
        {
            DestroyPlayer();
            GameManager.Instance.IsGameWin = false;
            GameManager.Instance.ChangeState(GameState.Result);
        }

        public void UpdatePlayer()
        {
            SpawnedPlayer?.LogicUpdate();
        }

        public void FixedUpdatePlayer()
        {
            SpawnedPlayer?.PhysicsUpdate();
        }
    }
}