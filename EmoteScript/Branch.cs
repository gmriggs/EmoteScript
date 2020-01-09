using System.Collections.Generic;

using EmoteScript.Entity.Enum;

namespace EmoteScript
{
    public class Branch
    {
        public Emote Parent;

        public EmoteCategory Category;

        public List<Emote> Emotes;

        public Branch(Emote parent, EmoteCategory category)
        {
            Parent = parent;
            Category = category;

            Emotes = new List<Emote>();
        }

        public void Add(Emote emote)
        {
            Emotes.Add(emote);
        }

        public static List<EmoteCategory> GotoSet { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.GotoSet
        };
        
        public static List<EmoteCategory> Test { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.TestSuccess,
            EmoteCategory.TestFailure
        };

        public static List<EmoteCategory> Quest { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.QuestSuccess,
            EmoteCategory.QuestFailure,
        };

        public static List<EmoteCategory> Event { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.EventSuccess,
            EmoteCategory.EventFailure,
        };

        public static List<EmoteCategory> TestFellow { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.TestSuccess,
            EmoteCategory.TestNoFellow
        };

        public static List<EmoteCategory> QuestFellow { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.QuestSuccess,
            EmoteCategory.QuestFailure,
            EmoteCategory.QuestNoFellow
        };

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
                    foreach (var branch in _emote.Branches.Values)
                        sets.AddRange(branch.Flatten());
                }
            }
            return sets;
        }
    }
}
