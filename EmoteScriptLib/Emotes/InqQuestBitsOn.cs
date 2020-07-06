using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqQuestBitsOn : Emote
    {
        public InqQuestBitsOn() : base(EmoteType.InqQuestBitsOn)
        {
            Init();
        }

        public InqQuestBitsOn(string quest, int bits)

            : base(EmoteType.InqQuestBitsOn)
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
