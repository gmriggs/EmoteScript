using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
