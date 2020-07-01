using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class StampMyQuest : Emote
    {
        public StampMyQuest()
            
            : base(EmoteType.StampMyQuest)
        {

        }
        
        public StampMyQuest(string quest)

            : base(EmoteType.StampMyQuest)
        {
            Message = quest;
        }
    }
}
