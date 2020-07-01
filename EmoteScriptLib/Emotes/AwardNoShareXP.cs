using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class AwardNoShareXP : Emote
    {
        public AwardNoShareXP() : base(EmoteType.AwardNoShareXP)
        {

        }

        public AwardNoShareXP(long amount)
            
            : base(EmoteType.AwardNoShareXP)
        {
            Amount64 = amount;
        }
    }
}
