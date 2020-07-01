using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqContractsFull : Emote
    {
        public InqContractsFull() : base(EmoteType.InqContractsFull)
        {
            AddValidBranches(Branch.Test);
        }
    }
}
