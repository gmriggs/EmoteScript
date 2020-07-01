using System.Numerics;

namespace EmoteScriptLib.JSON
{
    public class Frame
    {
        public Vector3 origin { get; set; }
        public Quaternion angles { get; set; }

        public Frame()
        {
        }

        public Frame(EmoteScriptLib.Emote emote)
        {
            origin = new Vector3(emote.OriginX ?? 0, emote.OriginY ?? 0, emote.OriginZ ?? 0);

            angles = new Quaternion(emote.AnglesX ?? 0, emote.AnglesY ?? 0, emote.AnglesZ ?? 0, emote.AnglesW ?? 1);
        }
    }
}
