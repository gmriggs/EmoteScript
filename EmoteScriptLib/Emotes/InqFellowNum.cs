using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqFellowNum : Emote
    {
        public InqFellowNum() : base(EmoteType.InqFellowNum)
        {
            AddValidBranches(Branch.TestFellowNum);
        }
    }
}
