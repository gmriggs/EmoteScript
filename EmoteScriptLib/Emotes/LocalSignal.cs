using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class LocalSignal : Emote
    {
        public LocalSignal() : base(EmoteType.LocalSignal)
        {

        }
        
        public LocalSignal(string message)

            : base(EmoteType.LocalSignal)
        {
            Message = message;
        }
    }
}
