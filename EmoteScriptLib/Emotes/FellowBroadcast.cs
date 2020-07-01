using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
