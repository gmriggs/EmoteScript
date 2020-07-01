using System.Collections.Generic;

namespace EmoteScript.JSON
{
    public class EmoteSet_KeyValue
    {
        public int key { get; set; }
        public List<EmoteSet> value { get; set; }

        public EmoteSet_KeyValue()
        {
        }

        public EmoteSet_KeyValue(EmoteScript.EmoteSet emoteSet)
        {
            key = (int)emoteSet.Category;
            value = new List<EmoteSet>() { new EmoteSet(emoteSet) };
        }
    }
}
