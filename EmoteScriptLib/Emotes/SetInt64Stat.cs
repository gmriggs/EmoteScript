using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class SetInt64Stat : Emote
    {
        public SetInt64Stat()
            
            : base(EmoteType.SetInt64Stat)
        {

        }
        
        public SetInt64Stat(PropertyInt64 stat, long? amount = null)

            : base(EmoteType.SetInt64Stat)
        {
            Stat = (int)stat;
            Amount64 = amount;
        }
    }
}
