using System;

namespace EmoteScriptLib.Entity.Enum
{
    [Flags]
    public enum EmoteField: ulong
    {
        None            = 0x0,
        Delay           = 0x1,
        Extent          = 0x2,
        Motion          = 0x4,
        Message         = 0x8,
        TestString      = 0x10,
        Min             = 0x20,
        Max             = 0x40,
        Min64           = 0x80,
        Max64           = 0x100,
        MinFloat        = 0x200,
        MaxFloat        = 0x400,
        Stat            = 0x800,
        Display         = 0x1000,
        Amount          = 0x2000,
        Amount64        = 0x4000,
        HeroXP64        = 0x8000,
        Percent         = 0x10000,
        SpellId         = 0x20000,
        WealthRating    = 0x40000,
        TreasureClass   = 0x80000,
        TreasureType    = 0x100000,
        PScript         = 0x200000,
        Sound           = 0x400000,
        DestinationType = 0x800000,
        WeenieClassId   = 0x1000000,
        StackSize       = 0x2000000,
        Palette         = 0x4000000,
        Shade           = 0x8000000,
        TryToBond       = 0x10000000,
        ObjCellId       = 0x20000000,
        OriginX         = 0x40000000,
        OriginY         = 0x80000000,
        OriginZ         = 0x100000000,
        AnglesW         = 0x200000000,
        AnglesX         = 0x400000000,
        AnglesY         = 0x800000000,
        AnglesZ         = 0x1000000000,

        // extended
        Position        = 0x2000000000,
        OriginAngles    = 0x4000000000,
        Angles          = 0x8000000000,
        Range           = 0x10000000000,
        Range64         = 0x20000000000,
        RangeFloat      = 0x40000000000,

        PropertyAttributeStat    = 0x80000000000,
        PropertyAttribute2ndStat = 0x100000000000,
        PropertyBoolStat         = 0x200000000000,
        PropertyFloatStat        = 0x400000000000,
        PropertyIntStat          = 0x800000000000,
        PropertyInt64Stat        = 0x1000000000000,
        PropertyStringStat       = 0x2000000000000,
        SkillStat                = 0x4000000000000,

        CharacterTitle           = 0x8000000000000,
        ContractId               = 0x10000000000000,
    }
}
