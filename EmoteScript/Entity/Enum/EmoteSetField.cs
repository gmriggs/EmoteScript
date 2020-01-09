using System;

namespace EmoteScript.Entity.Enum
{
    [Flags]
    public enum EmoteSetField
    {
        None          = 0x0,
        Probability   = 0x1,
        WeenieClassId = 0x2,
        Style         = 0x4,
        Substyle      = 0x8,
        Quest         = 0x10,
        VendorType    = 0x20,
        MinHealth     = 0x40,
        MaxHealth     = 0x80
    }
}
