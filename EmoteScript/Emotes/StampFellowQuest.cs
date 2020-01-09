using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class StampFellowQuest : Emote
    {
        public StampFellowQuest()
            
            : base(EmoteType.StampFellowQuest)
        {

        }
        
        public StampFellowQuest(string quest)

            : base(EmoteType.StampFellowQuest)
        {
            Message = quest;
        }
    }
}
