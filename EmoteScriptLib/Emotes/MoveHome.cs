using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
