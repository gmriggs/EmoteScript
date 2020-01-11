using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqEvent : Emote
    {
        public InqEvent()
            
            : base(EmoteType.InqEvent)
        {
            Init();
        }
        
        public InqEvent(string eventName)

            : base(EmoteType.InqEvent)
        {
            Init();

            Message = eventName;

        }

        public void Init()
        {
            AddValidBranches(Branch.Event);
        }
    }
}
