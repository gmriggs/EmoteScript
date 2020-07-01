using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class IncrementQuest : Emote
    {
        public IncrementQuest() : base(EmoteType.IncrementQuest)
        {

        }
        
        public IncrementQuest(string quest, int? amount = 1)

            : base(EmoteType.IncrementQuest)
        {
            Message = quest;
            Amount = amount;
        }
    }
}
