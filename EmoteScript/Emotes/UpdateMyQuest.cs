using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class UpdateMyQuest : Emote
    {
        public UpdateMyQuest() : base(EmoteType.UpdateMyQuest)
        {
            Init();
        }
        
        public UpdateMyQuest(string quest)

            : base(EmoteType.UpdateMyQuest)
        {
            Init();
            
            Message = quest;
        }

        public void Init()
        {
            AddBranches(Branch.Quest);
        }
    }
}
