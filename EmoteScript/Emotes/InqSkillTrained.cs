using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqSkillTrained : Emote
    {
        public InqSkillTrained() : base(EmoteType.InqSkillTrained)
        {
            Init();
        }
        
        public InqSkillTrained(Skill skill)

            : base(EmoteType.InqSkillTrained)
        {
            Init();
            
            Stat = (int)skill;
        }

        public void Init()
        {
            AddBranches(Branch.Test);
        }
    }
}
