using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
