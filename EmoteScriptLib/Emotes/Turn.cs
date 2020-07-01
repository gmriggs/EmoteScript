using System.Numerics;

using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class Turn : Emote
    {
        public Turn()
            
            : base(EmoteType.Turn)
        {

        }
        
        public Turn(Quaternion angle)

            : base(EmoteType.Turn)
        {
            SetOrientation(angle);
        }
    }
}
