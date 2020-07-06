using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class SetQuestBitsOff : Emote
    {
        public SetQuestBitsOff() : base(EmoteType.SetQuestBitsOff)
        {

        }

        public SetQuestBitsOff(string quest, int bits)

            : base(EmoteType.SetQuestBitsOff)
        {
            Message = quest;
            Amount = bits;
        }
    }
}
