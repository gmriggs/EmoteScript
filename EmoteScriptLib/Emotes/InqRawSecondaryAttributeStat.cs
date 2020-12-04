using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqRawSecondaryAttributeStat : Emote
    {
        public InqRawSecondaryAttributeStat()
            
            : base(EmoteType.InqRawSecondaryAttributeStat)
        {
            Init();
        }

        public InqRawSecondaryAttributeStat(PropertyAttribute2nd stat, int min, int max)

            : base(EmoteType.InqRawSecondaryAttributeStat)
        {
            Init();
            
            Stat = (int)stat;

            Min = min;
            Max = max;
        }

        public void Init()
        {
            AddValidBranches(Branch.TestQuality);
        }
    }
}
