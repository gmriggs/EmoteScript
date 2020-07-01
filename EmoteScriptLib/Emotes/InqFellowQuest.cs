using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqFellowQuest : Emote
    {
        public InqFellowQuest() : base(EmoteType.InqFellowQuest)
        {
            Init();
        }
        
        public InqFellowQuest(string quest)

            : base(EmoteType.InqFellowQuest)
        {
            Init();
            
            Message = quest;
        }

        public void Init()
        {
            AddValidBranches(Branch.QuestFellow);
        }
    }
}
