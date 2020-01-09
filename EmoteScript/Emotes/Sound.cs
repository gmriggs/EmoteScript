using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
