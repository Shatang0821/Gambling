using System.Collections.Generic;
using FrameWork.Component;
using Framework.Entity;

namespace Game.SkillSystem
{
    public class SkillProcessor
    {
        private SkillData _skillData;                 // スキルデータ
        private EntityObject _owner;                 // スキル所有者
        private List<SkillActionData> _pendingActions; // 実行待ちのアクション
        private float _currentTime;                  // 現在のスキル経過時間
        private bool _isRunning;                     // スキルが実行中かどうか

        public SkillProcessor(EntityObject owner)
        {
            _owner = owner;
            _pendingActions = new List<SkillActionData>();
        }
        
        /// <summary>
        /// スキルを設定する（解析を開始）
        /// </summary>
        /// <param name="skillData">実行するスキルデータ</param>
        public void SetSkill(SkillData skillData)
        {
            _skillData = skillData;
            _pendingActions.Clear();
            _pendingActions.AddRange(skillData.Actions); // 全アクションを登録
            _pendingActions.Sort((a, b) => a.TimeStamp.CompareTo(b.TimeStamp)); // TimeStamp順に並べ替え
            _currentTime = 0f;
            _isRunning = true;
        }
        
    }
}