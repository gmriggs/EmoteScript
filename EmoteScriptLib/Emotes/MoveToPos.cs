using EmoteScript.Entity;
using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
