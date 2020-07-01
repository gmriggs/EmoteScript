using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class UpdateFellowQuest : Emote
    {
        public UpdateFellowQuest() : base(EmoteType.UpdateFellowQuest)
        {
            Init();
        }
        
        public UpdateFellowQuest(string quest)

            : base(EmoteType.UpdateFellowQuest)
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
