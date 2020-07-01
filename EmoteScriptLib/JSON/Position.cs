namespace EmoteScriptLib.JSON
{
    public class Position
    {
        public Frame frame { get; set; }
        public uint objcell_id { get; set; }

        public Position()
        {
        }

        public Position(EmoteScriptLib.Emote emote)
        {
            objcell_id = emote.ObjCellId ?? 0;

            frame = new Frame(emote);
        }
    }
}
