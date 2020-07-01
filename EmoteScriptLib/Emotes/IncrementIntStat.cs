using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class IncrementIntStat : Emote
    {
        public IncrementIntStat() : base(EmoteType.IncrementIntStat)
        {

        }
        
        public IncrementIntStat(PropertyInt stat, int? amount = 1)

            : base(EmoteType.IncrementIntStat)
        {
            Stat = (int)stat;
            Amount = amount;
        }
    }
}
