using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqYesNo : Emote
    {
        public InqYesNo()
            
            : base(EmoteType.InqYesNo)
        {
            Init();
        }

        public InqYesNo(string message)

            : base(EmoteType.InqYesNo)
        {
            Init();

            TestString = message;
        }

        public void Init()
        {
            AddValidBranches(Branch.Test);
        }
    }
}
