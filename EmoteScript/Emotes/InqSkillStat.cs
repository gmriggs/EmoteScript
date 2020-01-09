using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqSkillStat : Emote
    {
        public InqSkillStat() : base(EmoteType.InqSkillStat)
        {
            Init();
        }
        
        public InqSkillStat(Skill skill, int min, int max)

            : base(EmoteType.InqSkillStat)
        {
            Init();
            
            Stat = (int)skill;

            Min = min;
            Max = max;
        }

        public void Init()
        {
            AddBranches(Branch.Test);
        }
    }
}
