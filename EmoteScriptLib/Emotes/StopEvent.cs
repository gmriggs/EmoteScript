using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class StopEvent : Emote
    {
        public StopEvent() : base(EmoteType.StopEvent)
        {

        }
        
        public StopEvent(string eventName)

            : base(EmoteType.StopEvent)
        {
            Message = eventName;
        }
    }
}
