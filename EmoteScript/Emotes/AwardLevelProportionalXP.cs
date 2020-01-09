using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class AwardLevelProportionalXP : Emote
    {
        public AwardLevelProportionalXP() : base(EmoteType.AwardLevelProportionalXP)
        {

        }
        
        public AwardLevelProportionalXP(float percent, long? max = null, bool? shareable = false)

            : base(EmoteType.AwardLevelProportionalXP)
        {
            Percent = percent;
            Max64 = max;
            Display = shareable;
        }
    }
}
