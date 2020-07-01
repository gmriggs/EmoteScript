using System.Collections.Generic;

namespace EmoteScriptLib.JSON
{
    public class EmoteSet_KeyValue
    {
        public int key { get; set; }
        public List<EmoteSet> value { get; set; }

        public EmoteSet_KeyValue()
        {
        }

        public EmoteSet_KeyValue(EmoteScriptLib.EmoteSet emoteSet)
        {
            key = (int)emoteSet.Category;
            value = new List<EmoteSet>() { new EmoteSet(emoteSet) };
        }
    }
}
