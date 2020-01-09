using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
