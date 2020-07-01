using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqPackSpace : Emote
    {
        public InqPackSpace() : base(EmoteType.InqPackSpace)
        {
            Init();
        }
        
        public InqPackSpace(int amount = 1)

            : base(EmoteType.InqPackSpace)
        {
            Init();
            
            Amount = amount;
        }

        public void Init()
        {
            AddValidBranches(Branch.Test);
        }
    }
}
