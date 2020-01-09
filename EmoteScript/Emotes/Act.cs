using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
