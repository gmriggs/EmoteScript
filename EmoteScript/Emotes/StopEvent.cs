using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
