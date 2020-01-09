using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqMyQuest : Emote
    {
        public InqMyQuest() : base(EmoteType.InqMyQuest)
        {
            Init();
        }
        
        public InqMyQuest(string quest)

            : base(EmoteType.InqMyQuest)
        {
            Init();

            Message = quest;
        }

        public void Init()
        {
            AddBranches(Branch.Test);
        }
    }
}
