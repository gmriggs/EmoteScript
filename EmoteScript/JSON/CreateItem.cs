using System;

namespace EmoteScript.JSON
{
    public class CreateItem
    {
        public uint? wcid { get; set; }
        public uint? palette { get; set; }
        public double? shade { get; set; }
        public uint? destination { get; set; }
        public int? stack_size { get; set; }
        public byte? try_to_bond { get; set; }

        public CreateItem(EmoteScript.Emote emote)
        {
            wcid = emote.WeenieClassId;
            palette = (uint?)emote.Palette;
            shade = emote.Shade;
            destination = (uint?)emote.DestinationType;
            stack_size = emote.StackSize;

            if (emote.TryToBond != null)
                try_to_bond = Convert.ToByte(emote.TryToBond.Value);
        }
    }
}
