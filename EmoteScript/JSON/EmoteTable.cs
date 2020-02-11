using System.Collections.Generic;
using System.Linq;

namespace EmoteScript.JSON
{
    public class EmoteTable
    {
        public List<EmoteSet_KeyValue> emoteTable { get; set; }

        public EmoteTable(EmoteScript.EmoteTable baseTable)
        {
            emoteTable = new List<EmoteSet_KeyValue>();

            foreach (var emoteSet in baseTable.EmoteSets)
            {
                var set = emoteTable.FirstOrDefault(i => i.key == (int)emoteSet.Category);

                if (set == null)
                    emoteTable.Add(new EmoteSet_KeyValue(emoteSet));
                else
                    set.value.Add(new EmoteSet(emoteSet));
            }
        }
    }
}
