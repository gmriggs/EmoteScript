using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
