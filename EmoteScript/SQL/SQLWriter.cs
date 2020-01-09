using System;
using System.Collections.Generic;
using System.Text;

namespace EmoteScript.SQL
{
    public static class SQLWriter
    {
        public static List<string> GetSQL(List<EmoteSet> emoteSets, string objectId)
        {
            var sqlLines = new List<string>();

            foreach (var emoteSet in emoteSets)
            {
                var categoryStr = $"{(int)emoteSet.Category} /* {emoteSet.Category} */";
                var probability = GetSQLString(emoteSet.Probability);
                var weenieClassIdStr = GetSQLString(emoteSet.WeenieClassId);
                var styleStr = emoteSet.Style != null ? $"{(int)emoteSet.Style} /* {emoteSet.Style} */" : "NULL";
                var substyleStr = emoteSet.Substyle != null ? $"{(int)emoteSet.Substyle} /* {emoteSet.Substyle} */" : "NULL";
                var quest = GetSQLString(emoteSet.Quest);
                var vendorTypeStr = emoteSet.VendorType != null ? $"{(int)emoteSet.VendorType} /* {emoteSet.VendorType} */" : "NULL";
                var minHealth = GetSQLString(emoteSet.MinHealth);
                var maxHealth = GetSQLString(emoteSet.MaxHealth);

                sqlLines.Add("INSERT INTO `weenie_properties_emote` (`object_Id`, `category`, `probability`, `weenie_Class_Id`, `style`, `substyle`, `quest`, `vendor_Type`, `min_Health`, `max_Health`)");
                sqlLines.Add($"VALUES ({objectId}, {categoryStr}, {probability}, {weenieClassIdStr}, {styleStr}, {substyleStr}, {quest}, {vendorTypeStr}, {minHealth}, {maxHealth});");
                sqlLines.Add(string.Empty);

                sqlLines.Add("SET @parent_id = LAST_INSERT_ID();");
                sqlLines.Add(string.Empty);

                var emoteSqlLines = GetSQL(emoteSet.Emotes);

                sqlLines.AddRange(emoteSqlLines);
            }

            return sqlLines;
        }

        public static List<string> GetSQL(List<Emote> emotes)
        {
            var sqlLines = new List<string>();

            sqlLines.Add("INSERT INTO `weenie_properties_emote_action` (`emote_Id`, `order`, `type`, `delay`, `extent`, `motion`, `message`, `test_String`, `min`, `max`, `min_64`, `max_64`, `min_Dbl`, `max_Dbl`, `stat`, `display`, `amount`, `amount_64`, `hero_X_P_64`, `percent`, `spell_Id`, `wealth_Rating`, `treasure_Class`, `treasure_Type`, `p_Script`, `sound`, `destination_Type`, `weenie_Class_Id`, `stack_Size`, `palette`, `shade`, `try_To_Bond`, `obj_Cell_Id`, `origin_X`, `origin_Y`, `origin_Z`, `angles_W`, `angles_X`, `angles_Y`, `angles_Z`)");

            for (var i = 0; i < emotes.Count; i++)
            {
                var emote = emotes[i];

                var prefix = i == 0 ? "VALUES " : "     , ";

                var typeStr = $"{(int)emote.Type} /* {emote.Type} */";
                var delay = GetSQLString(emote.Delay);
                var extent = GetSQLString(emote.Extent);
                var motionStr = emote.Motion != null ? $"0x{(uint)emote.Motion:X8} /* {emote.Motion} */" : "NULL";
                var message = GetSQLString(emote.Message);
                var testString = GetSQLString(emote.TestString);
                var min = GetSQLString(emote.Min);
                var max = GetSQLString(emote.Max);
                var min64 = GetSQLString(emote.Min64);
                var max64 = GetSQLString(emote.Max64);
                var minDbl = GetSQLString(emote.MinFloat);
                var maxDbl = GetSQLString(emote.MaxFloat);
                var statStr = GetSQLString(emote.Stat);
                var display = emote.Display != null ? Convert.ToInt32(emote.Display).ToString() : "NULL";
                var amount = GetSQLString(emote.Amount);
                var amount64 = GetSQLString(emote.Amount64);
                var heroXP64 = GetSQLString(emote.HeroXP64);
                var percent = GetSQLString(emote.Percent);
                var spellIdStr = emote.SpellId != null ? $"{(int)emote.SpellId} /* {emote.SpellId} */" : "NULL";
                var wealthRating = GetSQLString(emote.WealthRating);
                var treasureClass = GetSQLString(emote.TreasureClass);
                var treasureType = GetSQLString(emote.TreasureType);
                var pScriptStr = emote.PScript != null ? $"{(int)emote.PScript} /* {emote.PScript} */" : "NULL";
                var soundStr = emote.Sound != null ? $"{(int)emote.Sound} /* {emote.Sound} */" : "NULL";
                var destinationTypeStr = emote.DestinationType != null ? $"{(int)emote.DestinationType} /* {emote.DestinationType} */" : "NULL";
                var weenieClassIdStr = GetSQLString(emote.WeenieClassId);
                var stackSize = GetSQLString(emote.StackSize);
                var palette = GetSQLString(emote.Palette);
                var shade = GetSQLString(emote.Shade);
                var tryToBond = GetSQLString(emote.TryToBond);
                var objCellId = emote.ObjCellId != null ? $"0x{(uint)emote.ObjCellId:X8} /* {emote.ObjCellId} */" : "NULL";
                var originX = GetSQLString(emote.OriginX);
                var originY = GetSQLString(emote.OriginY);
                var originZ = GetSQLString(emote.OriginZ);
                var anglesW = GetSQLString(emote.AnglesW);
                var anglesX = GetSQLString(emote.AnglesX);
                var anglesY = GetSQLString(emote.AnglesY);
                var anglesZ = GetSQLString(emote.AnglesZ);

                sqlLines.Add($"{prefix}(@parent_id, {i}, {typeStr}, {delay}, {extent}, {motionStr}, {message}, {testString}, {min}, {max}, {min64}, {max64}, {minDbl}, {maxDbl}, {statStr}, {display}, {amount}, {amount64}, {heroXP64}, {percent}, {spellIdStr}, {wealthRating}, {treasureClass}, {treasureType}, {pScriptStr}, {soundStr}, {destinationTypeStr}, {weenieClassIdStr}, {stackSize}, {palette}, {shade}, {tryToBond}, {objCellId}, {originX}, {originY}, {originZ}, {anglesW}, {anglesX}, {anglesY}, {anglesZ});");
            }

            sqlLines.Add(string.Empty);

            return sqlLines;
        }

        public static string GetSQLString(object obj)
        {
            if (obj == null)
                return "NULL";
            else if (obj is string str)
                return $"'{str.Replace("'", "''")}'";
            else
                return obj.ToString();
        }
    }
}
