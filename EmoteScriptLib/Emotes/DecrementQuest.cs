using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
