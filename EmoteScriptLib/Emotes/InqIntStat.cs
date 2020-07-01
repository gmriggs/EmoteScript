using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqIntStat : Emote
    {
        public InqIntStat() : base(EmoteType.InqIntStat)
        {
            Init();
        }
        
        public InqIntStat(PropertyInt stat, int min, int max)

            : base(EmoteType.InqIntStat)
        {
            Init();
            
            Stat = (int)stat;
            Min = min;
            Max = max;
        }

        public void Init()
        {
            AddValidBranches(Branch.Test);
        }
    }
}
