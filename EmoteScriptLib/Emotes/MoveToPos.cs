using EmoteScriptLib.Entity;
using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class MoveToPos : Emote
    {
        public MoveToPos()
            
            : base(EmoteType.MoveToPos)
        {

        }
        
        public MoveToPos(Position position)

            : base(EmoteType.MoveToPos)
        {
            SetPosition(position);
        }
    }
}
