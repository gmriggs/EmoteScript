using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class FellowBroadcast : Emote
    {
        public FellowBroadcast()
            
            : base(EmoteType.FellowBroadcast)
        {

        }
        
        public FellowBroadcast(string message)

            : base(EmoteType.FellowBroadcast)
        {
            Message = message;
        }
    }
}
