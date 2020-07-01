using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqQuest : Emote
    {
        public InqQuest() : base(EmoteType.InqQuest)
        {
            Init();
        }
        
        public InqQuest(string quest)

            : base(EmoteType.InqQuest)
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
