using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InflictVitaePenalty : Emote
    {
        public InflictVitaePenalty()
            
            : base(EmoteType.InflictVitaePenalty)
        {

        }

        public InflictVitaePenalty(int amount)

            : base(EmoteType.InflictVitaePenalty)
        {
            Amount = amount;
        }
    }
}
