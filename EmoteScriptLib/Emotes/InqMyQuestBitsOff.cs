using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqMyQuestBitsOff : Emote
    {
        public InqMyQuestBitsOff() : base(EmoteType.InqMyQuestBitsOff)
        {
            Init();
        }

        public InqMyQuestBitsOff(string quest, int bits)

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
