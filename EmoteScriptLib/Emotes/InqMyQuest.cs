using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
            AddValidBranches(Branch.Quest);
        }
    }
}
