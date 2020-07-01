using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class Sound : Emote
    {
        public Sound()
            
            : base(EmoteType.Sound)
        {

        }
        
        public Sound(Entity.Enum.Sound sound)

            : base(EmoteType.Sound)
        {
            Sound = sound;
        }
    }
}
