using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
