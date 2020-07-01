using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
