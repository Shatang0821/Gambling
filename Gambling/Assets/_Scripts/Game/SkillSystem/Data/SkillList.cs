using UnityEngine;

namespace Game.SkillSystem
{
    [CreateAssetMenu(fileName = "SkillDataTable",menuName = "Data/SkillTable")]
    public class SkillList : ScriptableObject
    {
        public SkillData[] SkillDatas;
    }
}