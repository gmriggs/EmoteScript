using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqSecondaryAttributeStat : Emote
    {
        public InqSecondaryAttributeStat()
            
            : base(EmoteType.InqSecondaryAttributeStat)
        {
            Init();
        }
        
        public InqSecondaryAttributeStat(PropertyAttribute2nd stat, int min, int max)

            : base(EmoteType.InqSecondaryAttributeStat)
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
