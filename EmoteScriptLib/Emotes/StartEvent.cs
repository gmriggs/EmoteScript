using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class StartEvent : Emote
    {
        public StartEvent()
            
            : base(EmoteType.StartEvent)
        {

        }
        
        public StartEvent(string eventName)

            : base(EmoteType.StartEvent)
        {
            Message = eventName;
        }
    }
}
