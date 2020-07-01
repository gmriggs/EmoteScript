using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class AwardLuminance : Emote
    {
        public AwardLuminance()
            
            : base(EmoteType.AwardLuminance)
        {

        }
        
        public AwardLuminance(long amount)
            
            : base(EmoteType.AwardLuminance)
        {
            HeroXP64 = amount;
        }
    }
}
