namespace EmoteScript
{
    public class EmoteSetIndent
    {
        public EmoteSet EmoteSet;
        public int Indent;

        public EmoteSetIndent(EmoteSet emoteSet, int indent = 0)
        {
            EmoteSet = emoteSet;
            Indent = indent;
        }
    }
}
