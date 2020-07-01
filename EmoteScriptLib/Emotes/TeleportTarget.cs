using EmoteScriptLib.Entity;
using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
