using System.Collections.Generic;
using System.Linq;

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

        public EmoteSet()
        {

        }
        
        public EmoteSet(EmoteCategory category)
        {
            Category = category;
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

        public EmoteSet(Branch branch)
        {
            Category = branch.Category;
            Quest = branch.Parent.Message;
            // other filters?
        }

        public void Add(Emote emote)
        {
            Emotes.Add(emote);
        }

        public List<string> BuildScript()
        {
            var lines = new List<string>();
            lines.Add($"{Category}:");

            foreach (var emote in Emotes)
                lines.AddRange(emote.BuildScript());

            return lines;
        }

        public override string ToString()
        {
            var fields = new List<string>();
            fields.Add($"Category: {Category}");

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

            return string.Join(", ", fields);
        }

        public List<EmoteSet> Flatten()
        {
            var sets = new List<EmoteSet>();
            
            var emoteSet = new EmoteSet(this);
            sets.Add(emoteSet);

            foreach (var _emote in Emotes)
            {
                var emote = new Emote(_emote);
                emote.EmoteSet = emoteSet;

                emoteSet.Add(emote);
                
                if (_emote.HasBranches)
                {
                    // pull branches into root
                    foreach (var branch in _emote.Branches.Values)
                        sets.AddRange(branch.Flatten());
                }
            }

            return sets;
        }

        public static List<EmoteSet> Flatten(List<EmoteSet> emoteSets)
        {
            var flat = new List<EmoteSet>();

            foreach (var emoteSet in emoteSets)
                flat.AddRange(emoteSet.Flatten());

            return flat;
        }
    }
}
