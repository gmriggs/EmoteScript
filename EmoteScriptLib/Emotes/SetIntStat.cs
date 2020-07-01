using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class SetIntStat : Emote
    {
        public SetIntStat()
            
            : base(EmoteType.SetIntStat)
        {

        }
        
        public SetIntStat(PropertyInt stat, int? amount = null)

            : base(EmoteType.SetIntStat)
        {
            Stat = (int)stat;
            Amount = amount;
        }
    }
}
