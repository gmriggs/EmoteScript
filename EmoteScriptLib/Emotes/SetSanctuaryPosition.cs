using EmoteScript.Entity;
using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class SetSanctuaryPosition : Emote
    {
        public SetSanctuaryPosition() : base(EmoteType.SetSanctuaryPosition)
        {

        }

        public SetSanctuaryPosition(Position position)

            : base(EmoteType.SetSanctuaryPosition)
        {
            SetPosition(position);
        }
    }
}
