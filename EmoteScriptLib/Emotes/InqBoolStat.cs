using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
            AddValidBranches(Branch.TestQuality);
        }
    }
}
