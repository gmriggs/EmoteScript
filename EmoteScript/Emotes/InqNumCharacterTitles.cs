using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqNumCharacterTitles : Emote
    {
        public InqNumCharacterTitles() : base(EmoteType.InqNumCharacterTitles)
        {
            Init();
        }
        
        public InqNumCharacterTitles(int min, int max)

            : base(EmoteType.InqNumCharacterTitles)
        {
            Init();
            
            Min = min;
            Max = max;
        }

        public void Init()
        {
            AddBranches(Branch.Test);
        }
    }
}
