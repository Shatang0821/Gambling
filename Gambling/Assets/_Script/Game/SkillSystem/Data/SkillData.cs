using System.Collections.Generic;
using Framework.Aduio;
using UnityEngine;
using UnityEngine.Rendering;
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
        [Header("スキル継続時間")] 
        public float Duration;
        [Header("スキルアクション")]
        public List<SkillActionData> Actions; // スキルアクションリスト
        [FormerlySerializedAs("ComboWindow")] [Header("スキルコンボ")]
        public List<ComboData> ComboDatas;
    }

    
    [System.Serializable]
    public class SkillActionData
    {
        [Header("タイム")]
        public SkillActionType ActionType;      // アクションタイプ
        public float TimeStamp;                 // 実行タイミング
        public float Duration;                  // 継続時間
        [Header("ユーティリティ")] 
        public GameObject Prefab;               // プレハブをキー（必要に応じて）
        public AudioData AudioData;             // 効果音（必要に応じて）
        public float Value;                     // 効果値（ダメージ、距離など）
        public Vector3 Direction;               // オプションの方向ベクトル
        public bool IsPersistent;               // 継続動作かどうか（True: 継続、False: 単発）
        [Header("ターゲット設定")]
        public TargetSettings TargetSettings;   // 折り畳み対象
        [Header("フィードバック")] 
        public FeedBackData FeedBackData;       // フィードバックデータ
        public enum SkillActionType
        {
            TargetSelector,         // ダメージ
            Move,                   // 移動
            AudioPlay,              // ESを再生
            SpawnEffect,            // エフェクト生成
            CleanCollision          // 当たり判定の有無
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
        Circle,     // 円
        Box,        // 四角形
    }
    
    [System.Serializable]
    public class TargetSettings
    {
        [Header("ターゲット設定")]
        public TargetType TargetType;        // ターゲットタイプ
        public LayerMask TargetLayer;        // ターゲットレイヤ
        public ColliderType ColliderType;    // コライダータイプ
        public Vector3 Offset;               // コライダーのオフセット
        public float Radius;                 // 半径（Sphere、Capsule）
        public Vector3 Size;                 // サイズ（Box）
        public float Range;                  // 最大距離
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