using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
