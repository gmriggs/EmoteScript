using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqFloatStat : Emote
    {
        public InqFloatStat() : base(EmoteType.InqFloatStat)
        {
            Init();
        }
        
        public InqFloatStat(PropertyFloat stat, float min, float max)
            
            : base(EmoteType.InqFloatStat)
        {
            Init();
            
            Stat = (int)stat;
            MinFloat = min;
            MaxFloat = max;
        }

        public void Init()
        {
            AddValidBranches(Branch.TestQuality);
        }
    }
}
