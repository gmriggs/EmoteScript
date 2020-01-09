using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class TakeItems : Emote
    {
        public TakeItems() : base(EmoteType.TakeItems)
        {

        }
        
        public TakeItems(uint wcid, int stackSize = 1)

            : base(EmoteType.TakeItems)
        {
            WeenieClassId = wcid;
            StackSize = stackSize;
        }
    }
}
