using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class Goto : Emote
    {
        public Goto()
            
            : base(EmoteType.Goto)
        {

        }
        
        public Goto(string message)

            : base(EmoteType.Goto)
        {
            Message = message;

            AddValidBranches(Branch.GotoSet);
        }
    }
}
