using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class AwardLevelProportionalSkillXP : Emote
    {
        public AwardLevelProportionalSkillXP()
            
            : base(EmoteType.AwardLevelProportionalSkillXP)
        {

        }
        
        public AwardLevelProportionalSkillXP(Skill skill, float percent, int? max)
            
            : base(EmoteType.AwardLevelProportionalSkillXP)
        {
            Stat = (int)skill;
            Percent = percent;
            Max = max;
        }
    }
}
