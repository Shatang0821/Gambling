using System;
using Framework.Aduio;
using UnityEngine;

namespace Game.SkillSystem
{
    [Serializable]
    public class FeedBackData
    {
        [Header("ダメージ")] 
        public float Damage;
        
        [Header("カメラ")]
        public bool EnableCameraShake;       // カメラ震動を有効にするか
        public float ShakeIntensity;         // カメラ震動の強さ
        public float ShakeDuration;          // カメラ震動の継続時間

        [Header("フレーム")]
        public bool EnableTimeFreeze;        // フレームフリーズを有効にするか
        public float FreezeScale;            // タイムスケール
        public float FreezeDuration;         // フレームフリーズの時間 (秒)
        
        [Header("エフェクト")]
        public bool EnableEffect;            // エフェクトを生成するか
        public GameObject[] EffectPrefabs;   // エフェクトのPrefab
        public Vector2 EffectOffset;         // エフェクトの生成位置のオフセット

        [Header("音声関連")]
        public bool EnableAudio;             // 音声を再生するか
        public AudioData AudioData;          // 再生する音声データ
        
        [Header("ノックバック")]
        public bool EnableKnockback;         // ノックバックを有効にするか
        public float KnockbackForce;         // ノックバックの強さ
        public float KnockbackDuration;      // ノックバックの持続時間
    }
}