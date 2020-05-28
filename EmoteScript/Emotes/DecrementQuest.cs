using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class DecrementQuest : Emote
    {
        public DecrementQuest()

            : base(EmoteType.DecrementQuest)
        {

        }
        
        public DecrementQuest(string quest, int? amount = 1)

            : base(EmoteType.DecrementQuest)
        {
            Message = quest;
            Amount = amount;
        }
    }
}
