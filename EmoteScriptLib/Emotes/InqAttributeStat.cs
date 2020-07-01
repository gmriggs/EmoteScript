using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqAttributeStat : Emote
    {
        public InqAttributeStat() : base(EmoteType.InqAttributeStat)
        {
            Init();
        }
        
        public InqAttributeStat(PropertyAttribute stat, int min, int max)

            : base(EmoteType.InqAttributeStat)
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
