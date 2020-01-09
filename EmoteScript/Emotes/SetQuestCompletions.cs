using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class SetQuestCompletions : Emote
    {
        public SetQuestCompletions() : base(EmoteType.SetQuestCompletions)
        {

        }
        
        public SetQuestCompletions(string quest, int? amount)

            : base(EmoteType.SetQuestCompletions)
        {
            Message = quest;
            Amount = amount;
        }
    }
}
