using System;

using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class SetBoolStat : Emote
    {
        public SetBoolStat()
            
            : base(EmoteType.SetBoolStat)
        {

        }
        
        public SetBoolStat(PropertyBool stat, bool amount)

            : base(EmoteType.SetBoolStat)
        {
            Stat = (int)stat;
            Amount = Convert.ToInt32(amount);
        }
    }
}
