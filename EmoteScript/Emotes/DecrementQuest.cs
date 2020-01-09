using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class DecrementQuest : Emote
    {
        public DecrementQuest()

            : base(EmoteType.DecrementQuest)
        {

        }
        
        public DecrementQuest(string quest)

            : base(EmoteType.DecrementQuest)
        {
            Message = quest;
        }
    }
}
