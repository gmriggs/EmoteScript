using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class Tell : Emote
    {
        public Tell() : base(EmoteType.Tell)
        {

        }
        
        public Tell(string message) : base(EmoteType.Tell)
        {
            Message = message;
        }
    }
}
