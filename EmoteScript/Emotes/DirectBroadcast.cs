using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class DirectBroadcast : Emote
    {
        public DirectBroadcast() : base (EmoteType.DirectBroadcast)
        {

        }
        
        public DirectBroadcast(string message)

            : base (EmoteType.DirectBroadcast)
        {
            Message = message;
        }
    }
}
