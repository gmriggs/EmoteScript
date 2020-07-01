using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqFellowNum : Emote
    {
        public InqFellowNum() : base(EmoteType.InqFellowNum)
        {
            AddValidBranches(Branch.TestFellow);
        }
    }
}
