using System;

using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
