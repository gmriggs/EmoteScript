using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class InqBoolStat : Emote
    {
        public InqBoolStat() : base(EmoteType.InqBoolStat)
        {
            Init();
        }

        public InqBoolStat(PropertyBool stat)

            : base(EmoteType.InqBoolStat)
        {
            Init();

            Stat = (int)stat;
        }

        public void Init()
        {
            AddBranches(Branch.Test);
        }
    }
}
