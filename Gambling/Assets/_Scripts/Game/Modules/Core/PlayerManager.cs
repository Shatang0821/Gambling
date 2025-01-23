using System;
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
        }

        public void DestroyPlayer()
        {
            if(SpawnedPlayer == null) return;
            Destroy(SpawnedPlayer);
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