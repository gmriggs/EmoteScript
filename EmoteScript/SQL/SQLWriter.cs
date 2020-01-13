using System;
using System.Collections.Generic;

using EmoteScript.Entity;
using EmoteScript.Entity.Enum;

using EmoteScript.StringMap;

namespace EmoteScript.SQL
{
    public static class SQLWriter
    {
        public static List<string> GetSQL(EmoteTable emoteTable)
        {
            var sqlLines = new List<string>();

            foreach (var emoteSet in emoteTable.EmoteSets)
            {
                var objectId = emoteTable.Wcid != null ? emoteTable.Wcid.ToString() : "#####";
                var categoryStr = $"{(int)emoteSet.Category} /* {emoteSet.Category} */";
                var probability = emoteSet.Probability?.ToString() ?? "1";
                var weenieClassIdStr = GetSQLString(emoteSet.WeenieClassId) + GetWeenieName(emoteSet.WeenieClassId);
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
                var delay = emote.Delay?.ToString() ?? "0";
                var extent = emote.Extent?.ToString() ?? "1";
                var motionStr = emote.Motion != null ? $"0x{(uint)emote.Motion:X8} /* {emote.Motion} */" : "NULL";
                var message = GetSQLString(emote.Message);
                var testString = GetSQLString(emote.TestString);
                var min = GetSQLString(emote.Min);
                var max = GetSQLString(emote.Max);
                var min64 = GetSQLString(emote.Min64);
                var max64 = GetSQLString(emote.Max64);
                var minDbl = GetSQLString(emote.MinFloat);
                var maxDbl = GetSQLString(emote.MaxFloat);
                var statStr = GetSQLString(emote.Stat) + GetStatName(emote.Stat, emote.Type);
                var display = emote.Display != null ? Convert.ToInt32(emote.Display).ToString() : "NULL";
                var amountStr = GetSQLString(emote.Amount) + GetAmountName(emote.Amount, emote.Type);
                var amount64 = GetSQLString(emote.Amount64);
                var heroXP64 = GetSQLString(emote.HeroXP64);
                var percent = GetSQLString(emote.Percent);
                var spellIdStr = emote.SpellId != null ? (int)emote.SpellId + GetSpellName(emote.SpellId) : "NULL";
                var wealthRating = GetSQLString(emote.WealthRating);
                var treasureClass = GetSQLString(emote.TreasureClass);
                var treasureType = GetSQLString(emote.TreasureType);
                var pScriptStr = emote.PScript != null ? $"{(int)emote.PScript} /* {emote.PScript} */" : "NULL";
                var soundStr = emote.Sound != null ? $"{(int)emote.Sound} /* {emote.Sound} */" : "NULL";
                var destinationTypeStr = emote.DestinationType != null ? $"{(int)emote.DestinationType} /* {emote.DestinationType} */" : "NULL";
                var weenieClassIdStr = GetSQLString(emote.WeenieClassId) + GetWeenieName(emote.WeenieClassId);
                var stackSize = GetSQLString(emote.StackSize);
                var palette = GetSQLString(emote.Palette);
                var shade = GetSQLString(emote.Shade);
                var tryToBond = GetSQLString(emote.TryToBond);
                var objCellId = emote.ObjCellId != null ? $"0x{(uint)emote.ObjCellId:X8}" : "NULL";
                var teleloc = GetTeleLoc(emote.Position);
                var originX = GetSQLString(emote.OriginX);
                var originY = GetSQLString(emote.OriginY);
                var originZ = GetSQLString(emote.OriginZ);
                var anglesW = GetSQLString(emote.AnglesW);
                var anglesX = GetSQLString(emote.AnglesX);
                var anglesY = GetSQLString(emote.AnglesY);
                var anglesZ = GetSQLString(emote.AnglesZ);

                var postfix = i < emotes.Count - 1 ? "" : ";";
                
                sqlLines.Add($"{prefix}(@parent_id, {i}, {typeStr}, {delay}, {extent}, {motionStr}, {message}, {testString}, {min}, {max}, {min64}, {max64}, {minDbl}, {maxDbl}, {statStr}, {display}, {amountStr}, {amount64}, {heroXP64}, {percent}, {spellIdStr}, {wealthRating}, {treasureClass}, {treasureType}, {pScriptStr}, {soundStr}, {destinationTypeStr}, {weenieClassIdStr}, {stackSize}, {palette}, {shade}, {tryToBond}, {objCellId}{teleloc}, {originX}, {originY}, {originZ}, {anglesW}, {anglesX}, {anglesY}, {anglesZ}){postfix}");
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

        public static Dictionary<uint, string> WeenieNames;
        public static Dictionary<uint, string> WeenieClassNames;
        
        public static string GetWeenieName(uint? wcid)
        {
            if (wcid == null)
                return "";

            if (WeenieNames == null)
                WeenieNames = Reader.GetIDToNames("WeenieName.txt");

            if (WeenieClassNames == null)
                WeenieClassNames = Reader.GetIDToNames("WeenieClassName.txt");

            if (WeenieNames.TryGetValue(wcid.Value, out var weenieName))
                return $" /* {weenieName} */";

            if (WeenieClassNames.TryGetValue(wcid.Value, out var weenieClassName))
                return $" /* {weenieClassName} */";

            return "";
        }

        public static Dictionary<uint, string> SpellNames;

        public static string GetSpellName(SpellId? spellId)
        {
            if (spellId == null)
                return "";
            
            if (SpellNames == null)
                SpellNames = Reader.GetIDToNames("SpellName.txt");

            if (SpellNames.TryGetValue((uint)spellId, out var spellName))
                return $" /* {spellName} */";

            return "";
        }

        public static string GetTeleLoc(Position pos)
        {
            if (pos == null || pos.ObjCellId == 0 || pos.Frame == null)
                return "";

            return $" /* {pos} */";
        }

        public static string GetStatName(int? stat, EmoteType type)
        {
            if (stat == null)
                return "";

            switch (type)
            {
                case EmoteType.AwardLevelProportionalSkillXP:
                case EmoteType.AwardSkillPoints:
                case EmoteType.AwardSkillXP:

                case EmoteType.InqSkillStat:
                case EmoteType.InqRawSkillStat:
                case EmoteType.InqSkillTrained:
                case EmoteType.InqSkillSpecialized:
                case EmoteType.UntrainSkill:
                    return $" /* {(Skill)stat} */";

                case EmoteType.DecrementIntStat:
                case EmoteType.IncrementIntStat:
                case EmoteType.InqIntStat:
                case EmoteType.SetIntStat:
                    return $" /* {(PropertyInt)stat} */";

                case EmoteType.InqAttributeStat:
                case EmoteType.InqRawAttributeStat:
                    return $" /* {(PropertyAttribute)stat} */";

                case EmoteType.InqBoolStat:
                case EmoteType.SetBoolStat:
                    return $" /* {(PropertyBool)stat} */";

                case EmoteType.InqFloatStat:
                case EmoteType.SetFloatStat:
                    return $" /* {(PropertyFloat)stat} */";

                case EmoteType.InqInt64Stat:
                case EmoteType.SetInt64Stat:
                    return $" /* {(PropertyInt64)stat} */";

                case EmoteType.InqSecondaryAttributeStat:
                case EmoteType.InqRawSecondaryAttributeStat:
                    return $" /* {(PropertyAttribute2nd)stat} */";

                case EmoteType.InqStringStat:
                    return $" /* {(PropertyAttribute2nd)stat} */";

                default:
                    return "";
            }
        }

        public static string GetAmountName(int? amount, EmoteType type)
        {
            if (amount == null)
                return "";

            switch (type)
            {
                case EmoteType.AddCharacterTitle:
                    return $" /* {(CharacterTitle)amount} */";

                case EmoteType.AddContract:
                case EmoteType.RemoveContract:
                    return $" /* {(ContractId)amount} */";

                default:
                    return "";
            }
        }
    }
}
