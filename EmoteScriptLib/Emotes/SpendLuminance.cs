using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class SpendLuminance : Emote
    {
        public SpendLuminance() : base(EmoteType.SpendLuminance)
        {

        }
        
        public SpendLuminance(long amount)

            : base(EmoteType.SpendLuminance)
        {
            HeroXP64 = amount;
        }
    }
}
