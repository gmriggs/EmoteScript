using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqQuestBitsOff : Emote
    {
        public InqQuestBitsOff() : base(EmoteType.InqQuestBitsOff)
        {
            Init();
        }

        public InqQuestBitsOff(string quest, int bits)

            : base(EmoteType.InqMyQuestBitsOff)
        {
            Init();

            Message = quest;
            Amount = bits;
        }

        public void Init()
        {
            AddValidBranches(Branch.Quest);
        }
    }
}
