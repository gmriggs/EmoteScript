using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class DecrementMyQuest : Emote
    {
        public DecrementMyQuest()

            : base(EmoteType.DecrementMyQuest)
        {

        }
        
        public DecrementMyQuest(string quest, int? amount = 1)

            : base(EmoteType.DecrementMyQuest)
        {
            Message = quest;
            Amount = amount;
        }
    }
}
