using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class PhysScript: Emote
    {
        public PhysScript() : base(EmoteType.PhysScript)
        {

        }
        
        public PhysScript(PlayScript pscript, float? extent = null)

            : base(EmoteType.PhysScript)
        {
            PScript = pscript;
            Extent = extent;
        }
    }
}
