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
        public uint? classID { get; set; }
        public uint? style { get; set; }
        public uint? substyle { get; set; }
        public float? minhealth { get; set; }
        public float? maxhealth { get; set; }

        public EmoteSet()
        {
        }

        public EmoteSet(EmoteScriptLib.EmoteSet emoteSet)
        {
            category = (uint)emoteSet.Category;
            probability = emoteSet.Probability ?? 1.0f;
            vendorType = (uint?)emoteSet.VendorType;
            quest = emoteSet.Quest;
            classID = emoteSet.WeenieClassId;
            style = (uint?)emoteSet.Style;
            substyle = (uint?)emoteSet.Substyle;
            minhealth = emoteSet.MinHealth;
            maxhealth = emoteSet.MaxHealth;

            emotes = new List<Emote>();

            foreach (var emote in emoteSet.Emotes)
                emotes.Add(new Emote(emote));
        }
    }
}
