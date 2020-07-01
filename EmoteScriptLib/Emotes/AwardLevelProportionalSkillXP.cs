using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class AwardLevelProportionalSkillXP : Emote
    {
        public AwardLevelProportionalSkillXP()
            
            : base(EmoteType.AwardLevelProportionalSkillXP)
        {

        }
        
        public AwardLevelProportionalSkillXP(Skill skill, float percent, long? max)
            
            : base(EmoteType.AwardLevelProportionalSkillXP)
        {
            Stat = (int)skill;
            Percent = percent;
            Max64 = max;
        }
    }
}
