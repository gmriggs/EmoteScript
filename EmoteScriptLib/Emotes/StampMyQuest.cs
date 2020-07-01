using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
