using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class AwardSkillXP : Emote
    {
        public AwardSkillXP() : base(EmoteType.AwardSkillXP)
        {

        }
        
        public AwardSkillXP(Skill skill, int amount)

            : base(EmoteType.AwardSkillXP)
        {
            Stat = (int)skill;
            Amount = amount;
        }
    }
}
