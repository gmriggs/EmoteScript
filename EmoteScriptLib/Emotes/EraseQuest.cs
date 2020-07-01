using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class EraseQuest : Emote
    {
        public EraseQuest() : base(EmoteType.EraseQuest)
        {

        }
        
        public EraseQuest(string quest)

            : base(EmoteType.EraseQuest)
        {
            Message = quest;
        }
    }
}
