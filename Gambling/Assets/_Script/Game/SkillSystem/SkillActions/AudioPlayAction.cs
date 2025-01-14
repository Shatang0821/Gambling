using Framework.Aduio;
using Framework.Entity;

namespace Game.SkillSystem.Actions
{
    public class AudioPlayAction : ISkillAction
    {
        public SkillActionData SkillActionData { get; }
        public float TimeStamp => SkillActionData?.TimeStamp ?? 0f;
        public float Duration => SkillActionData?.Duration ?? 0f;
        public bool IsPersistent  => SkillActionData.IsPersistent; // データから取得
        
        public AudioPlayAction(SkillActionData skillActionData)
        {
            SkillActionData = skillActionData;
        }
        
        public void Enter(EntityObject owner)
        {
            var audioData = SkillActionData.AudioData;
            if (audioData.IsPlayRandomPitch)
            {
                AudioManager.Instance.PlayRandomSFX(audioData);
            }
            else
            {
                AudioManager.Instance.PlaySFX(audioData);
            }
        }

        public void Update(EntityObject owner)
        {
            
        }

        public void Exit(EntityObject owner)
        {
            
        }
    }
}