using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.SkillSystem
{
    [CreateAssetMenu(fileName = "SkillData",menuName = "Data/Skill")]
    public class SkillData : ScriptableObject
    {
        [Header("基本パラメータ")]
        public int SkillID;                  // スキルID
        public string SkillName;             // スキル名
        public float Cooldown;               // クールダウン時間
        public float ManaCost;               // 消費マナ
        public string AnimationName;         // アニメーション名
        public bool CanBeInterrupted;        // 中断できるか
        [Header("ターゲット設定")]
        public TargetType TargetType;        // 対象タイプ
        public ColliderType ColliderType;    // コライダータイプ
        public Vector3 Offset;               // コライダーのオフセット
        public float Radius;                 // 半径（Sphere、Capsule）
        public Vector3 Size;                 // サイズ（Box）
        public float Range;                  // 最大距離

        [Header("スキル継続時間")] 
        public float Duration;
        
        
        [Header("スキルアクション")]
        public List<SkillActionData> Actions; // スキルアクションリスト
        
        [Header("スキルコンボ")]
        public List<ComboData> ComboWindow;
        private void OnValidate()
        {
            // 自身対象の場合、コライダーの設定を無効化
            if (TargetType == TargetType.Self)
            {
                ColliderType = ColliderType.Sphere;
                Offset = Vector3.zero;
                Radius = 0f;
                Size = Vector3.zero;
            }
            // 敵や範囲対象の場合、適切なフィールドをリセット
            else if (TargetType != TargetType.Self)
            {
                if (ColliderType == ColliderType.Sphere)
                {
                    Size = Vector3.zero; // Boxのサイズをリセット
                }
                else if (ColliderType == ColliderType.Box)
                {
                    Radius = 0f; // Sphereの半径をリセット
                }
            }
        }
    }
    
    [System.Serializable]
    public class SkillActionData
    {
        public SkillActionType ActionType;  // アクションタイプ
        public float TimeStamp;             // 実行タイミング
        public float Duration;              // 継続時間
        public string EffectPrefabName;     // プレハブ名（必要に応じて）
        public float Value;                 // 効果値（ダメージ、距離など）
        public Vector3 Direction;           // オプションの方向ベクトル

        public enum SkillActionType
        {
            Damage,         // ダメージ
            Heal,           // 回復
            SpawnPrefab,    // プレハブ生成
            Move            // 移動
        }
    }
    
    public enum TargetType
    {
        Self,       // 自身
        Enemy,      // 単体敵
        AOE         // 範囲
    }

    public enum ColliderType
    {
        Sphere,     // 球体
        Box,        // 立方体
        Capsule     // カプセル
    }
    
    [System.Serializable]
    public class ComboData
    {
        public float StartTime;      // 開始時間
        public float EndTime;        // 終了時間
        public int NextSkillID;      // 次のスキルID
        public string InputKey;      // 入力もしくは条件（例"Attack"）
    }
    
}