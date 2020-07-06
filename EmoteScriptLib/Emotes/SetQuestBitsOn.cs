using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class SetQuestBitsOn : Emote
    {
        public SetQuestBitsOn() : base(EmoteType.SetQuestBitsOn)
        {

        }

        public SetQuestBitsOn(string quest, int bits)

            : base(EmoteType.SetQuestBitsOn)
        {
            Message = quest;
            Amount = bits;
        }
    }
}
