using System.Collections.Generic;

using EmoteScript.Entity.Enum;
using EmoteScript.StringMap;

using Newtonsoft.Json;

namespace EmoteScript
{
    public class EmoteSet
    {
        public EmoteCategory Category { get; set; }
        public float? Probability { get; set; }
        public uint? WeenieClassId { get; set; }
        public MotionStance? Style { get; set; }
        public MotionCommand? Substyle { get; set; }
        public string Quest { get; set; }
        public VendorType? VendorType { get; set; }
        public float? MinHealth { get; set; }
        public float? MaxHealth { get; set; }

        public List<Emote> Emotes { get; set; } = new List<Emote>();

        /// <summary>
        /// The branch emotes that can possibly link to this EmoteSet
        /// </summary>
        [JsonIgnore]
        public List<Emote> Links { get; set; }

        public EmoteSet()
        {

        }
        
        public EmoteSet(EmoteCategory category, Emote parent = null)
        {
            Category = category;

            if (parent != null)
                AddLink(parent);
        }

        public EmoteSet(JSON.EmoteSet emoteSet)
        {
            Category = (EmoteCategory)emoteSet.category;
            Probability = emoteSet.probability;
            VendorType = (VendorType?)emoteSet.vendorType;
            Quest = emoteSet.quest;
            WeenieClassId = emoteSet.classId;
            Style = (MotionStance?)emoteSet.style;
            Substyle = (MotionCommand?)emoteSet.subStyle;
            MinHealth = emoteSet.minHealth;
            MaxHealth = emoteSet.maxHealth;

            foreach (var emote in emoteSet.emotes)
                Emotes.Add(new Emote(emote));
        }

        public void AddLink(Emote link)
        {
            if (Links == null)
                Links = new List<Emote>();

            Links.Add(link);
        }

        public void Add(Emote emote)
        {
            Emotes.Add(emote);
        }

        public override string ToString()
        {
            var result = $"{Category}:";

            var filters = GetFilters();
            
            if (filters.Count > 0)
                result += " " + string.Join(", ", filters);

            return result;
        }

        public List<string> GetFilters(bool excludeQuest = false)
        {
            var fields = new List<string>();

            if (WeenieClassId != null)
                fields.Add($"{WeenieName}");
            if (Style != null)
                fields.Add($"Style: {Style}");
            if (Substyle != null)
                fields.Add($"Substyle: {Substyle}");
            if (Quest != null && !excludeQuest)
                fields.Add($"{Quest}");
            if (VendorType != null)
                fields.Add($"VendorType: {VendorType}");
            if (Probability != null)
                fields.Add($"Probability: {Probability}");
            if (MinHealth != null)
                fields.Add($"MinHealth: {MinHealth}");
            if (MaxHealth != null)
                fields.Add($"MaxHealth: {MaxHealth}");

            return fields;
        }

        public string ToString(bool fluent)
        {
            if (!fluent)
                return ToString();

            var result = $"{Category}:";

            var fluentStr = GetFluentString();

            if (fluentStr.Length > 0)
                result += $" {fluentStr}";

            return result;
        }

        public string GetFluentString()
        {
            var excludeQuest = Links != null && Inline;

            return string.Join(", ", GetFilters(excludeQuest));
        }

        public static Dictionary<uint, string> WeenieNames;
        public static Dictionary<uint, string> WeenieClassNames;

        [JsonIgnore]
        public string WeenieName
        {
            get
            {
                if (WeenieClassId == null)
                    return null;

                if (WeenieNames == null)
                    WeenieNames = Reader.GetIDToNames("WeenieName.txt");

                if (WeenieClassNames == null)
                    WeenieClassNames = Reader.GetIDToNames("WeenieClassName.txt");

                if (WeenieNames.TryGetValue(WeenieClassId.Value, out var weenieName))
                    return $"{weenieName} ({WeenieClassId})";

                if (WeenieClassNames.TryGetValue(WeenieClassId.Value, out var weenieClassName))
                    return $"{weenieClassName} ({WeenieClassId})";

                return WeenieClassId.ToString();
            }
        }

        [JsonIgnore]
        public bool Inline => Links != null && Links.Count == 1;

        public void NormalRange()
        {
            if (MinHealth != null && MaxHealth == null)
                MaxHealth = float.MaxValue;

            if (MaxHealth != null && MinHealth == null)
                MinHealth = float.MinValue;
        }
    }
}
