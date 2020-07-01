using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
