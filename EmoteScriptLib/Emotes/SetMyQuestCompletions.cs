using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
