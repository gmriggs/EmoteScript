using System.Collections.Generic;

using Newtonsoft.Json.Serialization;

namespace EmoteScript.JSON
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        public static Dictionary<string, string> KeyMap = new Dictionary<string, string>()
        {
            { "VendorType", "vendorType" },
            { "WeenieClassId", "classID" },
            { "Message", "msg" },
            { "MinFloat", "fmin" },
            { "MaxFloat", "fmax" },
            // position
            // frame
            { "WealthRating", "wealth_rating" },
            { "TreasureClass", "treasure_class" },
            { "TreasureType", "treasure_type" },
        };
        
        protected override string ResolvePropertyName(string propertyName)
        {
            if (KeyMap.TryGetValue(propertyName, out var resolveName))
                return resolveName;
            else
                return propertyName.ToLower();
        }
    }
}
