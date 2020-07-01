using EmoteScriptLib.Entity;
using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
