using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class SetMyQuestBitsOn : Emote
    {
        public SetMyQuestBitsOn() : base(EmoteType.SetMyQuestBitsOn)
        {

        }

        public SetMyQuestBitsOn(string quest, int bits)

            : base(EmoteType.SetMyQuestBitsOn)
        {
            Message = quest;
            Amount = bits;
        }
    }
}
