using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
            AddValidBranches(Branch.TestQuality);
        }
    }
}
