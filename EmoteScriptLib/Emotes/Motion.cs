using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class Motion : Emote
    {
        public Motion() : base(EmoteType.Motion)
        {

        }
        
        public Motion(MotionCommand motion, float? extent = null)

            : base(EmoteType.Motion)
        {
            Motion = motion;
            Extent = extent;
        }
    }
}
