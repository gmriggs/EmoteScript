using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqRawSkillStat : Emote
    {
        public InqRawSkillStat() : base(EmoteType.InqRawSkillStat)
        {
            Init();
        }
        
        public InqRawSkillStat(Skill skill, int min, int max)

            : base(EmoteType.InqRawSkillStat)
        {
            Init();
            
            Stat = (int)skill;

            Min = min;
            Max = max;
        }

        public void Init()
        {
            AddValidBranches(Branch.Test);
        }
    }
}
