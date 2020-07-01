using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class IncrementMyQuest : Emote
    {
        public IncrementMyQuest() : base(EmoteType.IncrementMyQuest)
        {

        }
        
        public IncrementMyQuest(string quest, int? amount = 1)

            : base(EmoteType.IncrementMyQuest)
        {
            Message = quest;
            Amount = amount;
        }
    }
}
