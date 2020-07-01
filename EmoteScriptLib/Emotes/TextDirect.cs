using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class TextDirect : Emote
    {
        public TextDirect()
            
            : base(EmoteType.TextDirect)
        {

        }
        
        public TextDirect(string message)

            : base(EmoteType.TextDirect)
        {
            Message = message;
        }
    }
}
