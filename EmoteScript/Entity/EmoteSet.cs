using System.Collections.Generic;

using Newtonsoft.Json;

using EmoteScript.Entity.Enum;

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

        public void AddLink(Emote link)
        {
            if (Links == null)
                Links = new List<Emote>();

            Links.Add(link);
        }

        public EmoteSet(EmoteSet emoteSet)
        {
            // shallow copy constructor
            Category = emoteSet.Category;
            Probability = emoteSet.Probability;
            WeenieClassId = emoteSet.WeenieClassId;
            Style = emoteSet.Style;
            Substyle = emoteSet.Substyle;
            Quest = emoteSet.Quest;
            VendorType = emoteSet.VendorType;
            MinHealth = emoteSet.MinHealth;
            MaxHealth = emoteSet.MaxHealth;
        }

        public void Add(Emote emote)
        {
            Emotes.Add(emote);
        }

        public override string ToString()
        {
            var fields = new List<string>();

            if (Probability != null)
                fields.Add($"Probability: {Probability}");
            if (WeenieClassId != null)
                fields.Add($"WeenieClassId: {WeenieClassId}");
            if (Style != null)
                fields.Add($"Style: {Style}");
            if (Substyle != null)
                fields.Add($"Substyle: {Substyle}");
            if (Quest != null)
                fields.Add($"Quest: {Quest}");
            if (VendorType != null)
                fields.Add($"VendorType: {VendorType}");
            if (MinHealth != null)
                fields.Add($"MinHealth: {MinHealth}");
            if (MaxHealth != null)
                fields.Add($"MaxHealth: {MaxHealth}");

            var result = $"{Category}:";
            
            if (fields.Count > 0)
                result += " " + string.Join(", ", fields);

            return result;
        }
    }
}
