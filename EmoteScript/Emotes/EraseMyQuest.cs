using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class EraseMyQuest : Emote
    {
        public EraseMyQuest() : base(EmoteType.EraseMyQuest)
        {

        }

        public EraseMyQuest(string quest)

            : base(EmoteType.EraseMyQuest)
        {
            Message = quest;
        }
    }
}
