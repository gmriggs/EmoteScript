using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
