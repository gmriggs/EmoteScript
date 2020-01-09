using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class LocalSignal : Emote
    {
        public LocalSignal() : base(EmoteType.LocalSignal)
        {

        }
        
        public LocalSignal(string message)

            : base(EmoteType.LocalSignal)
        {
            TestString = message;
        }
    }
}
