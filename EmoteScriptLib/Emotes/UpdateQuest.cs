using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class UpdateQuest : Emote
    {
        public UpdateQuest() : base(EmoteType.UpdateQuest)
        {
            Init();
        }
        
        public UpdateQuest(string quest)

            : base(EmoteType.UpdateQuest)
        {
            Init();
            
            Message = quest;
        }

        public void Init()
        {
            AddValidBranches(Branch.Quest);
        }
    }
}
