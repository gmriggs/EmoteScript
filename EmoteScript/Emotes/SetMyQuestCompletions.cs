using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class SetMyQuestCompletions : Emote
    {
        public SetMyQuestCompletions() : base(EmoteType.SetMyQuestCompletions)
        {

        }
        
        public SetMyQuestCompletions(string quest, int? amount)

            : base(EmoteType.SetMyQuestCompletions)
        {
            Message = quest;
            Amount = amount;
        }
    }
}
