using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class DecrementMyQuest : Emote
    {
        public DecrementMyQuest()

            : base(EmoteType.DecrementMyQuest)
        {

        }
        
        public DecrementMyQuest(string quest)

            : base(EmoteType.DecrementMyQuest)
        {
            Message = quest;
        }
    }
}
