using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class LocalBroadcast : Emote
    {
        public LocalBroadcast()
            
            : base(EmoteType.LocalBroadcast)
        {

        }
        
        public LocalBroadcast(string message)
            
            : base(EmoteType.LocalBroadcast)
        {
            Message = message;
        }
    }
}
