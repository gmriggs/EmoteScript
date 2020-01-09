using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqSkillSpecialized : Emote
    {
        public InqSkillSpecialized() : base(EmoteType.InqSkillSpecialized)
        {
            Init();
        }
        
        public InqSkillSpecialized(Skill skill)

            : base(EmoteType.InqSkillSpecialized)
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
