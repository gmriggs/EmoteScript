using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class Give : Emote
    {
        public Give()
            
            : base(EmoteType.Give)
        {

        }
        
        public Give(uint wcid, int stackSize = 1)

            : base(EmoteType.Give)
        {
            WeenieClassId = wcid;
            StackSize = stackSize;
        }
    }
}
