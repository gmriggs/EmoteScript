using System;

namespace EmoteScriptLib.JSON
{
    public class Emote
    {
        public uint type { get; set; }
        public float? delay { get; set; }
        public float? extent { get; set; }
        public uint? amount { get; set; }
        public uint? motion { get; set; }
        public string msg { get; set; }
        public long? amount64 { get; set; }
        public ulong? heroxp64 { get; set; }
        public CreateItem cprof { get; set; }
        public long? min64 { get; set; }
        public long? max64 { get; set; }
        public float? percent { get; set; }
        public byte? display { get; set; }
        public uint? max { get; set; }
        public uint? min { get; set; }
        public float? fmax { get; set; }
        public float? fmin { get; set; }
        public uint? stat { get; set; }
        public uint? pscript { get; set; }
        public uint? sound { get; set; }
        public Position mPosition { get; set; }
        public Frame frame { get; set; }
        public uint? spellid { get; set; }
        public string teststring { get; set; }
        public uint? wealth_rating { get; set; }
        public uint? treasure_class { get; set; }
        public uint? treasure_type { get; set; }

        public Emote()
        {
        }

        public Emote(EmoteScriptLib.Emote emote)
        {
            type = (uint)emote.Type;
            delay = emote.Delay ?? 0.0f;
            extent = emote.Extent ?? (emote.Type == Entity.Enum.EmoteType.Say ? 0.0f : 1.0f);
            amount = (uint?)emote.Amount;
            motion = (uint?)emote.Motion;
            msg = emote.Message;
            amount64 = emote.Amount64;
            heroxp64 = (uint?)emote.HeroXP64;

            if (emote.WeenieClassId != null || emote.Palette != null || emote.Shade != null || emote.DestinationType != null || emote.StackSize != null || emote.TryToBond != null)
                cprof = new CreateItem(emote);

            min64 = emote.Min64;
            max64 = emote.Max64;
            percent = (float?)emote.Percent;
            
            if (emote.Display != null)
                display = Convert.ToByte(emote.Display.Value);

            max = (uint?)emote.Max;
            min = (uint?)emote.Min;
            fmax = emote.MaxFloat;
            fmin = emote.MinFloat;
            stat = (uint?)emote.Stat;
            pscript = (uint?)emote.PScript;
            sound = (uint?)emote.Sound;

            if (emote.ObjCellId != null || emote.OriginX != null || emote.OriginY != null || emote.OriginZ != null)
            {
                mPosition = new Position(emote);
            }
            else if (emote.AnglesW != null || emote.AnglesX != null | emote.AnglesY != null || emote.AnglesZ != null)
            {
                frame = new Frame(emote);
            }

            spellid = (uint?)emote.SpellId;
            teststring = emote.TestString;
            wealth_rating = (uint?)emote.WealthRating;
            treasure_class = (uint?)emote.TreasureClass;
            treasure_type = (uint?)emote.TreasureType;
        }
    }
}
