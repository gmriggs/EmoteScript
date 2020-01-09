using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class MoveHome : Emote
    {
        public MoveHome()
            
            : base(EmoteType.MoveHome)
        {

        }
        
        public MoveHome(float? extent = null)

            : base(EmoteType.MoveHome)
        {
            Extent = extent;
        }
    }
}
