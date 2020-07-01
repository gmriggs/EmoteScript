using System.Collections.Generic;

namespace EmoteScriptLib.JSON
{
    public class EmoteSet
    {
        public uint category { get; set; }
        public List<Emote> emotes { get; set; }
        public float? probability { get; set; }
        public uint? vendorType { get; set; }
        public string quest { get; set; }
        public uint? classId { get; set; }
        public uint? style { get; set; }
        public uint? subStyle { get; set; }
        public float? minHealth { get; set; }
        public float? maxHealth { get; set; }

        public EmoteSet()
        {
        }

        public EmoteSet(EmoteScriptLib.EmoteSet emoteSet)
        {
            category = (uint)emoteSet.Category;
            probability = emoteSet.Probability ?? 1.0f;
            vendorType = (uint?)emoteSet.VendorType;
            quest = emoteSet.Quest;
            classId = emoteSet.WeenieClassId;
            style = (uint?)emoteSet.Style;
            subStyle = (uint?)emoteSet.Substyle;
            minHealth = emoteSet.MinHealth;
            maxHealth = emoteSet.MaxHealth;

            emotes = new List<Emote>();

            foreach (var emote in emoteSet.Emotes)
                emotes.Add(new Emote(emote));
        }
    }
}
