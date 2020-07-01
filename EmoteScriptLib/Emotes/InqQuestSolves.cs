using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqQuestSolves : Emote
    {
        public InqQuestSolves() : base(EmoteType.InqQuestSolves)
        {
            Init();
        }
        
        public InqQuestSolves(string quest, int min, int max)

            : base(EmoteType.InqQuestSolves)
        {
            Init();
            
            Message = quest;

            Min = min;
            Max = max;
        }

        public void Init()
        {
            AddValidBranches(Branch.Quest);
        }
    }
}
