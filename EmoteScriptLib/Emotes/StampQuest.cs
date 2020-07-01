using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class StampQuest : Emote
    {
        public StampQuest() : base(EmoteType.StampQuest)
        {

        }
        
        public StampQuest(string quest)

            : base(EmoteType.StampQuest)
        {
            Message = quest;
        }
    }
}
