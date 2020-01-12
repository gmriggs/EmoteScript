using System;
using System.Collections.Generic;
using System.Linq;

namespace EmoteScript
{
    public class EmoteTable
    {
        public uint? Wcid;
        
        public List<EmoteSet> EmoteSets;

        public EmoteTable()
        {
            EmoteSets = new List<EmoteSet>();
        }

        public void Add(EmoteSet emoteSet)
        {
            EmoteSets.Add(emoteSet);
        }

        /// <summary>
        /// - Discovers which Emotes can link to which EmoteSets
        /// - Adds links between those Emotes and EmoteSets
        /// </summary>
        public void BuildLinks()
        {
            var emotes = GetBranches();

            foreach (var emote in emotes)
            {
                emote.Branches = GetLinks(emote);

                foreach (var branch in emote.Branches)
                    branch.AddLink(emote);
            }
        }

        /// <summary>
        /// Returns a list of all the branching emotes in this emote table
        /// </summary>
        public List<Emote> GetBranches()
        {
            var branches = new List<Emote>();

            foreach (var emoteSet in EmoteSets)
                branches.AddRange(emoteSet.Emotes.Where(i => i.HasBranches));

            return branches;
        }

        /// <summary>
        /// Returns a list of EmoteSets
        /// that this Emote can possibly branch to
        /// </summary>
        public List<EmoteSet> GetLinks(Emote emote)
        {
            var links = new List<EmoteSet>();
            
            foreach (var emoteSet in EmoteSets)
            {
                if (!emote.ValidBranches.Contains(emoteSet.Category))
                    continue;

                if (!emoteSet.Quest.Equals(emote.Message))
                    continue;

                links.Add(emoteSet);
            }
            return links;
        }

        /// <summary>
        /// Clears all links between set branches
        /// </summary>
        public void ClearLinks()
        {
            foreach (var emoteSet in EmoteSets)
            {
                emoteSet.Links = null;

                foreach (var emote in emoteSet.Emotes)
                    emote.Branches = null;
            }
        }

        /// <summary>
        /// Sets the valid branch categories
        /// for every emote in this table
        /// </summary>
        public void SetValidBranches()
        {
            foreach (var emoteSet in EmoteSets)
            {
                foreach (var emote in emoteSet.Emotes)
                    emote.ValidBranches = Emote.GetValidBranches(emote.Type);
            }
        }
    }
}
