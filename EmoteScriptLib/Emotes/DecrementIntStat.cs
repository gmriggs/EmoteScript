using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class DecrementIntStat : Emote
    {
        public DecrementIntStat()
            
            : base(EmoteType.DecrementIntStat)
        {

        }

        public DecrementIntStat(PropertyInt stat, int? amount = 1)

            : base(EmoteType.DecrementIntStat)
        {
            Stat = (int)stat;
            Amount = amount;
        }
    }
}
