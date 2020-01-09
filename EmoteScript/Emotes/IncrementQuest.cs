using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class IncrementQuest : Emote
    {
        public IncrementQuest() : base(EmoteType.IncrementQuest)
        {

        }
        
        public IncrementQuest(string quest)

            : base(EmoteType.IncrementQuest)
        {
            Message = quest;
        }
    }
}
