using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqMyQuestSolves : Emote
    {
        public InqMyQuestSolves() : base(EmoteType.InqMyQuestSolves)
        {
            Init();
        }
        
        public InqMyQuestSolves(string quest, int min, int max)

            : base(EmoteType.InqMyQuestSolves)
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
