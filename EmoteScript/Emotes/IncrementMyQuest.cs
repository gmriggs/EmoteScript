using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class IncrementMyQuest : Emote
    {
        public IncrementMyQuest() : base(EmoteType.IncrementMyQuest)
        {

        }
        
        public IncrementMyQuest(string quest)

            : base(EmoteType.IncrementMyQuest)
        {
            Message = quest;
        }
    }
}
