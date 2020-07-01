using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class ForceMotion : Emote
    {
        public ForceMotion() : base(EmoteType.ForceMotion)
        {

        }
        
        public ForceMotion(MotionCommand motion, float? extent = null)

            : base(EmoteType.ForceMotion)
        {
            Motion = motion;
            Extent = extent;
        }
    }
}
