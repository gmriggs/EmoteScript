using System.Collections.Generic;

using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib
{
    public static class Branch
    {
        public static List<EmoteCategory> GotoSet { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.GotoSet
        };
        
        public static List<EmoteCategory> Test { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.TestSuccess,
            EmoteCategory.TestFailure
        };

        public static List<EmoteCategory> TestQuality { get; } = new List<EmoteCategory>()
        {
            EmoteCategory.TestSuccess,
            EmoteCategory.TestFailure,
            EmoteCategory.TestNoQuality
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
    }
}
