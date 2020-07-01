using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
