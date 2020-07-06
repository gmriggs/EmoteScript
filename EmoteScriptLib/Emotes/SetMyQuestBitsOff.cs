using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class SetMyQuestBitsOff : Emote
    {
        public SetMyQuestBitsOff() : base(EmoteType.SetMyQuestBitsOff)
        {

        }

        public SetMyQuestBitsOff(string quest, int bits)

            : base(EmoteType.SetMyQuestBitsOff)
        {
            Message = quest;
            Amount = bits;
        }
    }
}
