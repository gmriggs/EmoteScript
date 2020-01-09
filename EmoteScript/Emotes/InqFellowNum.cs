using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqFellowNum : Emote
    {
        public InqFellowNum() : base(EmoteType.InqFellowNum)
        {
            AddBranches(Branch.TestFellow);
        }
    }
}
