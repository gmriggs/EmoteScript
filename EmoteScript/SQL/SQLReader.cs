using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

using EmoteScript.Entity.Enum;

namespace EmoteScript.SQL
{
    public class SQLReader
    {
        public EmoteTable ReadEmoteTable(string[] sqlLines)
        {
            var emoteTable = new EmoteTable();
            
            bool? isEmote = null;
            var currentColumns = new List<string>();

            foreach (var line in sqlLines)
            {
                var match = Regex.Match(line, @"`class_Id` = (\d+);");
                if (match.Success)
                {
                    //Console.WriteLine($"Found wcid {Wcid}");
                    if (uint.TryParse(match.Groups[1].Value, out var wcid))
                        emoteTable.Wcid = wcid;
                    continue;
                }

                if (line.StartsWith("INSERT INTO"))
                {
                    var startEmoteSet = line.StartsWith("INSERT INTO `weenie_properties_emote`");
                    var startEmote = line.StartsWith("INSERT INTO `weenie_properties_emote_action`");

                    if (!startEmoteSet && !startEmote)
                    {
                        isEmote = null;
                        continue;
                    }

                    isEmote = startEmote;
                    currentColumns = GetColumns(line);
                    //Console.WriteLine($"Found columns: {string.Join(", ", CurrentColumns)}");
                    continue;

                }

                if (isEmote == null)
                    continue;

                if (line.Contains("SET @parent_id = LAST_INSERT_ID()"))
                    continue;

                if (line.Contains("("))
                {
                    var fields = GetFields(line);
                    //Console.WriteLine($"Found fields: {string.Join(", ", fields)}");

                    AddRecord(emoteTable.EmoteSets, currentColumns, fields, isEmote.Value);
                }
            }

            return emoteTable;
        }

        public void AddRecord(List<EmoteSet> emoteSets, List<string> currentColumns, List<string> fields, bool isEmote)
        {
            if (isEmote)
            {
                var emote = new Emote();
                PopulateFields(emote, currentColumns, fields);
                var currentSet = emoteSets.LastOrDefault();
                if (currentSet == null)
                {
                    Console.WriteLine($"SQLReader.AddRecord() - failed to add emote for unknown set");
                    return;
                }
                emote.ValidBranches = Emote.GetValidBranches(emote.Type);
                currentSet.Add(emote);
            }
            else
            {
                var emoteSet = new EmoteSet();
                PopulateFields(emoteSet, currentColumns, fields);
                emoteSets.Add(emoteSet);
            }
        }

        public static HashSet<string> SkipColumns = new HashSet<string>()
        {
            "EmoteId",
            "ObjectId",
            "Order"     // ensure pre-sort
        };

        public static Dictionary<string, string> DefaultValues = new Dictionary<string, string>()
        {
            { "Delay", "0" },
            { "DestinationType", "Undef" },
            { "Extent", "1" },
            { "Message", "" },
            { "HeroXP64", "0" },
            { "Palette", "0" },
            { "Probability", "1" },
            { "Shade", "0" },
            { "StackSize", "1" },
            { "TryToBond", "False" },
        };

        public void PopulateFields(object record, List<string> currentColumns, List<string> fields)
        {
            for (var i = 0; i < currentColumns.Count; i++)
            {
                if (fields[i] == "NULL")
                    continue;
                
                var column = currentColumns[i];
                var propName = GetPropertyName(column);

                if (propName.Equals("MinDbl"))
                    propName = "MinFloat";
                else if (propName.Equals("MaxDbl"))
                    propName = "MaxFloat";

                if (SkipColumns.Contains(propName))
                    continue;

                var prop = record.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                if (prop == null)
                {
                    Console.WriteLine($"AddRecord: couldn't find propName {propName} in WeeniePropertiesInt");
                    continue;
                }
                var value = GetValueType(prop, fields[i]);

                if (DefaultValues.TryGetValue(propName, out var defaultValue) && value.ToString().Equals(defaultValue))
                    continue;

                prop.SetValue(record, value, null);
            }
        }

        public static object GetValueType(PropertyInfo prop, string value)
        {
            if (prop.PropertyType.FullName.Contains("UInt32"))
            {
                if (!uint.TryParse(value, out var result))
                {
                    if (value.StartsWith("0x"))
                    {
                        value = value.Replace("0x", "");
                        if (!uint.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
                            Console.WriteLine($"Failed to parse {value} into UInt32");
                    }
                    else
                        Console.WriteLine($"Failed to parse {value} into UInt32");
                }
                return result;

            }
            else if (prop.PropertyType.FullName.Contains("Int32"))
            {
                if (!int.TryParse(value, out var result))
                {
                    if (value.StartsWith("0x"))
                    {
                        value = value.Replace("0x", "");
                        if (!int.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result))
                            Console.WriteLine($"Failed to parse {value} into Int32");
                    }
                    else
                        Console.WriteLine($"Failed to parse {value} into Int32");
                }
                return result;
            }
            else if (prop.PropertyType.FullName.Contains("String"))
                return value.Replace("''", "'");

            else if (prop.PropertyType.FullName.Contains("UInt64"))
            {
                if (!ulong.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into UInt64");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("Int64"))
            {
                if (!long.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into Int64");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("UInt16"))
            {
                if (!ushort.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into UInt16");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("Int16"))
            {
                if (!short.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into Int16");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("DateTime"))
            {
                if (!DateTime.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into DateTime");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("Boolean"))
            {
                if (!bool.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into Boolean");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("Single"))
            {
                if (!float.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into Float");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("Double"))
            {
                if (!double.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into Double");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("SByte"))
            {
                if (!sbyte.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into SByte");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("Byte"))
            {
                if (!byte.TryParse(value, out var result))
                    Console.WriteLine($"Failed to parse {value} into Byte");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("EmoteCategory"))
            {
                if (!Enum.TryParse(value, out EmoteCategory result))
                    Console.WriteLine($"Failed to parse {value} into EmoteCategory");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("EmoteType"))
            {
                if (!Enum.TryParse(value, out EmoteType result))
                    Console.WriteLine($"Failed to parse {value} into EmoteType");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("MotionStance"))
            {
                if (!Enum.TryParse(value, out MotionStance result))
                {
                    if (value.StartsWith("0x"))
                    {
                        value = value.Replace("0x", "");
                        if (!uint.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var iResult))
                            Console.WriteLine($"Failed to parse {value} into MotionStance");
                        
                        return (MotionStance)iResult;
                    }
                    else
                        Console.WriteLine($"Failed to parse {value} into MotionStance");
                }
                return result;
            }
            else if (prop.PropertyType.FullName.Contains("MotionCommand"))
            {
                if (!Enum.TryParse(value, out MotionCommand result))
                {
                    if (value.StartsWith("0x"))
                    {
                        value = value.Replace("0x", "");
                        if (!uint.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var iResult))
                            Console.WriteLine($"Failed to parse {value} into MotionCommand");

                        return (MotionCommand)iResult;
                    }
                    else
                        Console.WriteLine($"Failed to parse {value} into MotionCommand");
                }
                return result;
            }
            else if (prop.PropertyType.FullName.Contains("DestinationType"))
            {
                if (!Enum.TryParse(value, out DestinationType result))
                    Console.WriteLine($"Failed to parse {value} into DestinationType");

                return result;
            }
            else if (prop.PropertyType.FullName.Contains("Sound"))
            {
                if (!Enum.TryParse(value, out Sound result))
                    Console.WriteLine($"Failed to parse {value} into Sound");

                return result;
            }

            Console.WriteLine($"Unknown property type: {prop.PropertyType.Name}");
            
            return value;
        }

        public static string GetPropertyName(string column)
        {
            var words = column.Split('_');
            var result = "";
            foreach (var word in words)
                result += word[0].ToString().ToUpper()[0] + word.Substring(1);

            return result;
        }

        public static List<string> GetColumns(string line)
        {
            var startIdx = line.IndexOf('`', line.IndexOf('('));
            var columns = new List<string>();
            while (startIdx != -1)
            {
                var endIdx = line.IndexOf('`', startIdx + 1);
                if (endIdx == -1)
                {
                    Console.WriteLine($"GetColumns({line}): couldn't find end delimiter after column {startIdx}");
                    break;
                }
                columns.Add(line.Substring(startIdx + 1, endIdx - startIdx - 1));
                startIdx = line.IndexOf('`', endIdx + 1);
            }
            return columns;
        }

        public static List<string> GetFields(string line)
        {
            line = RemoveComments(line);

            var startIdx = line.IndexOf('(') + 1;
            var fields = new List<string>();
            bool done = false;

            while (startIdx != -1)
            {
                if (startIdx >= line.Length) break;

                bool isString = line[startIdx] == '\'' || line[startIdx] == '\"';

                var endIdx = -1;
                if (!isString)
                {
                    endIdx = line.IndexOf(',', startIdx + 1);
                    if (endIdx == -1)
                    {
                        endIdx = line.IndexOf(')', startIdx + 1);
                        if (endIdx == -1)
                            endIdx = line.Length;

                        done = true;
                    }
                }
                else
                {
                    var doubleQuotes = line[startIdx] == '\"';

                    var idx = startIdx + 1;
                    while (true)
                    {
                        var endChar = doubleQuotes ? '\"' : '\'';
                        
                        endIdx = line.IndexOf(endChar, idx);
                        if (endIdx == -1)
                        {
                            line = line + endChar;
                            endIdx = line.Length;
                            break;
                        }
                        else if (!doubleQuotes && line[endIdx + 1] == '\'')
                        {
                            idx = endIdx + 2;
                            continue;
                        }
                        else if (doubleQuotes && line[endIdx - 1] == '\\')
                        {
                            idx = endIdx + 1;
                            continue;
                        }
                        endIdx++;
                        break;
                    }
                }

                if (endIdx == -1)
                {
                    Console.WriteLine($"GetFields({line}): couldn't find end delimiter after column {startIdx}");
                    break;
                }
                var field = line.Substring(startIdx, endIdx - startIdx).Trim();
                if (field.StartsWith("'") && field.EndsWith("'") || field.StartsWith("\"") && field.EndsWith("\""))
                    field = field.Substring(1, field.Length - 2);

                fields.Add(field);
                startIdx = endIdx + 2;

                if (done) break;
            }
            return fields;
        }

        public static string RemoveComments(string line)
        {
            var startIdx = line.IndexOf("/*");
            while (startIdx != -1)
            {
                var endIdx = line.IndexOf("*/", startIdx + 1);
                if (endIdx == -1)
                {
                    Console.WriteLine($"RemoveComments({line}): couldn't find end delimiter after column {startIdx}");
                    break;
                }
                line = line.Substring(0, startIdx) + line.Substring(endIdx + 2);
                startIdx = line.IndexOf("/*");
            }
            return line;
        }
    }
}
