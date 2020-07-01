using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqContractsFull : Emote
    {
        public InqContractsFull() : base(EmoteType.InqContractsFull)
        {
            AddValidBranches(Branch.Test);
        }
    }
}
