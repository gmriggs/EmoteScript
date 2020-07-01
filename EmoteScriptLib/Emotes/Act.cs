using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class Act : Emote
    {
        public Act() : base(EmoteType.Act)
        {
        }
        
        public Act(string message) : base(EmoteType.Act)
        {
            Message = message;
        }
    }
}
