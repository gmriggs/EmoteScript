using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
