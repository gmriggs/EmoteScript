using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class SetFloatStat : Emote
    {
        public SetFloatStat() : base(EmoteType.SetFloatStat)
        {

        }
        
        public SetFloatStat(PropertyFloat stat, float? amount = null)

            : base(EmoteType.SetFloatStat)
        {
            Stat = (int)stat;
            Percent = amount;
        }
    }
}
