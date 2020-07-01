using System;

namespace EmoteScriptLib.Entity.Enum
{
    [Flags]
    public enum DestinationType
    {
        Undef           = 0x0,
        Contain         = 0x1,
        Wield           = 0x2,
        Shop            = 0x4,
        Treasure        = 0x8,
        HouseBuy        = 0x10,
        HouseRent       = 0x20,
        Checkpoint      = Contain | Wield | Shop,
        ContainTreasure = Contain | Treasure,
        WieldTreasure   = Wield | Treasure,
        ShopTreasure    = Shop | Treasure
    }
}
