using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class AwardSkillPoints : Emote
    {
        public AwardSkillPoints()
            
            : base(EmoteType.AwardSkillPoints)
        {

        }

        public AwardSkillPoints(Skill skill, int amount)

            :base(EmoteType.AwardSkillPoints)
        {
            Stat = (int)skill;
            Amount = amount;
        }
    }
}
