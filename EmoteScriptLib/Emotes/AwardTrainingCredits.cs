using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class AwardTrainingCredits : Emote
    {
        public AwardTrainingCredits() : base(EmoteType.AwardTrainingCredits)
        {

        }
        
        public AwardTrainingCredits(int skillCredits)

            : base(EmoteType.AwardTrainingCredits)
        {
            Amount = skillCredits;
        }
    }
}
