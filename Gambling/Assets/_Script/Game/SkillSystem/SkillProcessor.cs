using System.Collections.Generic;
using FrameWork.Component;
using Framework.Entity;

namespace Game.SkillSystem
{
    public class SkillProcessor
    {
        private SkillData _skillData;
        private EntityObject _owner;
        private List<SkillActionData> _pendingActions;

        public SkillProcessor(EntityObject owner)
        {
            _owner = owner;
        }
        
    }
}