using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class WorldBroadcast : Emote
    {
        public WorldBroadcast()
            
            : base(EmoteType.WorldBroadcast)
        {

        }
        
        public WorldBroadcast(string message)
            
            : base(EmoteType.WorldBroadcast)
        {
            Message = message;
        }
    }
}
