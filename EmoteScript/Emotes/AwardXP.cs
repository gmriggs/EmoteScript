using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class AwardXP : Emote
    {
        public AwardXP()
            
            : base(EmoteType.AwardXP)
        {

        }
        
        public AwardXP(long amount)

            : base(EmoteType.AwardXP)
        {
            Amount64 = amount;
        }
    }
}
