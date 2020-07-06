using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class InqMyQuestBitsOn : Emote
    {
        public InqMyQuestBitsOn() : base(EmoteType.InqMyQuestBitsOn)
        {
            Init();
        }

        public InqMyQuestBitsOn(string quest, int bits)

            : base(EmoteType.InqMyQuestBitsOn)
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
