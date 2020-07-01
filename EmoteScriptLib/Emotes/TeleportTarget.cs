using EmoteScript.Entity;
using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class TeleportTarget : Emote
    {
        public TeleportTarget()
            
            : base(EmoteType.TeleportTarget)
        {

        }
        
        public TeleportTarget(Position position)

            : base(EmoteType.TeleportTarget)
        {
            SetPosition(position);
        }
    }
}
