using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqStringStat : Emote
    {
        public InqStringStat() : base(EmoteType.InqStringStat)
        {
            Init();
        }
        
        public InqStringStat(PropertyString stat, string testString)

            : base(EmoteType.InqStringStat)
        {
            Init();
            
            Stat = (int)stat;

            TestString = testString;
        }

        public void Init()
        {
            AddValidBranches(Branch.Test);
        }
    }
}
