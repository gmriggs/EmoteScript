using System.Collections.Generic;

using EmoteScript.Entity;

namespace EmoteScript
{
    public class ScriptWriter
    {
        public List<EmoteSet> EmoteSets;

        public ScriptWriter(List<EmoteSet> emoteSets)
        {
            EmoteSets = emoteSets;
        }

        public List<string> BuildScript()
        {
            var lines = new List<string>();
            
            for (var i = 0; i < EmoteSets.Count; i++)
            {
                var emoteSet = EmoteSets[i];

                if (i > 0)
                    lines.Add(string.Empty);
                
                lines.AddRange(emoteSet.BuildScript());
            }
            return lines;
        }
    }
}
