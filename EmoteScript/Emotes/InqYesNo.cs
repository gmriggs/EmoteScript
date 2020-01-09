using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
            AddBranches(Branch.Test);
        }
    }
}
