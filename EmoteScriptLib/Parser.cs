using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

using EmoteScriptLib.Entity;
using EmoteScriptLib.Entity.Enum;
using EmoteScriptLib.StringMap;

namespace EmoteScriptLib
{
    public static class Parser
    {
        public static Dictionary<string, uint> SpellNames;
        
        public static bool Debug = true;
        
        public static List<EmoteSet> ParseFile(FileInfo file)
        {
            if (!file.Exists)
            {
                Console.WriteLine($"{file.FullName} not found");
                return null;
            }

            var lines = File.ReadAllLines(file.FullName);

            return ParseLines(lines);
        }

        public static List<EmoteSet> ParseLines(string[] lines)
        {
            var emoteSets = new List<EmoteSet>();

            var stack = new Stack<EmoteSetIndent>();

            for (var i = 0; i < lines.Length; i++)
            {
                //if (Debug)
                //Console.WriteLine(lines[i]);

                var pre = Line.Preprocess(lines[i]);
                var line = Line.Parse(lines[i]);

                if (line == null)
                    continue;

                var emoteSetLine = line as EmoteSet_Line;
                var emoteLine = line as Emote_Line;

                stack.TryPeek(out var emoteSetIndent);

                var emoteSet = emoteSetIndent?.EmoteSet;
                var indent = emoteSetIndent?.Indent;

                var emote = emoteSet?.Emotes.LastOrDefault();

                if (emoteSetLine != null)
                {
                    while (emote != null && (!emote.HasBranches || emote.HasBranches && emote.HasBranchesCompleted) || indent > GetIndentLevel(lines[i]))
                    {
                        stack.Pop();

                        // try to go back 1
                        stack.TryPeek(out emoteSetIndent);

                        emoteSet = emoteSetIndent?.EmoteSet;
                        indent = emoteSetIndent?.Indent;

                        emote = emoteSet?.Emotes.LastOrDefault();
                    }

                    if (emote != null && emote.HasBranches)
                    {
                        var category = emoteSetLine.EmoteSet.Category;

                        // ensure this emote set category is valid for this branch
                        if (!emote.ValidBranches.Contains(category))
                        {
                            ShowError($"{category} is invalid for {emote.Type}", lines, i);
                            return null;
                        }

                        // generate links
                        if (emoteSetLine.EmoteSet.Quest == null)
                            emoteSetLine.EmoteSet.Quest = emote.Message;

                        emote.Branches.Add(emoteSetLine.EmoteSet);

                        emoteSetLine.EmoteSet.AddLink(emote);
                    }

                    // start processing new emote set
                    emoteSets.Add(emoteSetLine.EmoteSet);

                    stack.Push(new EmoteSetIndent(emoteSetLine.EmoteSet, GetIndentLevel(lines[i])));
                }
                else if (emoteLine != null)
                {
                    // process emote
                    if (emoteSet == null)
                    {
                        ShowError("Couldn't find EmoteSet to add Emote", lines, i);
                        return null;
                    }

                    // add emote to current set
                    emoteSet.Add(emoteLine.Emote);
                    emoteSetIndent.Indent = GetIndentLevel(lines[i]);
                }
            }

            return emoteSets;
        }

        public static List<Line> ParseLines(FileInfo file)
        {
            if (!file.Exists)
            {
                Console.WriteLine($"{file.FullName} not found");
                return null;
            }

            var _lines = File.ReadAllLines(file.FullName);

            var lines = new List<Line>();
            foreach (var _line in _lines)
            {
                //if (Debug)
                //Console.WriteLine(_line);

                var line = Line.Parse(_line);
                lines.Add(line);

                if (Debug)
                {
                    if (line is Emote_Line emote)
                        Console.WriteLine(emote.Emote);
                    else if (line is EmoteSet_Line emoteSet)
                        Console.WriteLine(emoteSet.EmoteSet);
                }
            }

            return lines;
        }

        public static int GetIndentLevel(string line)
        {
            return line.Length - line.TrimStart().Length;
        }

        public static string PreprocessLine(string line)
        {
            line = line.Trim();

            if (line.StartsWith("-"))
                line = line.Substring(1).Trim();

            return line;
        }

        public static string ParseDirective(string line)
        {
            // assumes line has already been normalized,
            // ie. trimmed, - prefix removed
            var idx = line.IndexOf(':');
            if (idx == -1)
            {
                idx = line.IndexOf(' ');
                if (idx == -1)
                    return line;
            }
            
            var startIdx = 0;
            if (line.StartsWith("Delay"))
            {
                startIdx = line.IndexOf(",") + 2;
                idx -= startIdx;
            }

            return line.Substring(startIdx, idx);
        }

        public static EmoteSet TryParseEmoteSet(string line, string cmd)
        {
            if (!Enum.TryParse(cmd, true, out EmoteCategory category))
                return null;

            //Console.WriteLine(category);

            return new EmoteSet(category);
        }

        public static Emote TryParseEmote(string line, string cmd)
        {
            if (!Enum.TryParse(cmd, true, out EmoteType type))
                return null;

            //Console.WriteLine(type);

            return Factory.Create(type);
        }

        public static EmoteCategory? TryParseBranch(List<EmoteCategory> branches, string line, string cmd)
        {
            if (!Enum.TryParse(cmd, true, out EmoteCategory category))
                return null;

            if (!branches.Contains(category))
                return null;

            //Console.WriteLine(category);

            return category;
        }

        public static void ShowError(string msg, string[] lines, int lineNumber)
        {
            Console.WriteLine($"{msg} on line {lineNumber}");
            Console.WriteLine(lines[lineNumber].Trim());
        }

        public static float? TryParseFloat(string floatStr)
        {
            if (!float.TryParse(floatStr, out var floatVal))
            {
                Console.WriteLine($"TryParseFloat() - couldn't convert float from \"{floatStr}\"");
                return null;
            }
            return floatVal;
        }

        public static MotionCommand? TryParseMotionCommand(string motionStr)
        {
            motionStr = motionStr.Replace("Motion.", "", StringComparison.OrdinalIgnoreCase);
            motionStr = motionStr.Replace("MotionCommand.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(motionStr, true, out MotionCommand motion))
            {
                Console.WriteLine($"TryParseMotionCommand() - couldn't convert MotionCommand from \"{motionStr}\"");
                return null;
            }
            return motion;
        }


        public static MotionStance? TryParseMotionStance(string stanceStr)
        {
            stanceStr = stanceStr.Replace("MotionStance.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(stanceStr, true, out MotionStance stance))
            {
                Console.WriteLine($"TryParseMotionStance() - couldn't convert MotionStance from \"{stanceStr}\"");
                return null;
            }
            return stance;
        }

        public static int? TryParseInt(string intStr)
        {
            intStr = intStr.Replace(",", "");
            
            if (!int.TryParse(intStr, out var intVal))
            {
                Console.WriteLine($"TryParseInt() - couldn't convert int from \"{intStr}\"");
                return null;
            }
            return intVal;
        }

        public static long? TryParseInt64(string int64Str)
        {
            int64Str = int64Str.Replace(",", "");
            
            if (!long.TryParse(int64Str, out var int64Val))
            {
                Console.WriteLine($"TryParseLong() - couldn't convert long from \"{int64Str}\"");
                return null;
            }
            return int64Val;
        }

        public static bool? TryParseBool(string boolStr)
        {
            if (boolStr == "0")
                return false;
            else if (boolStr == "1")
                return true;
            
            if (!bool.TryParse(boolStr, out var boolVal))
            {
                Console.WriteLine($"TryParseBool() - couldn't convert bool from \"{boolStr}\"");
                return null;
            }
            return boolVal;
        }

        public static double? TryParseDouble(string doubleStr)
        {
            if (!double.TryParse(doubleStr, out var doubleVal))
            {
                Console.WriteLine($"TryParseDouble() - couldn't convert double from \"{doubleStr}\"");
                return null;
            }
            return doubleVal;
        }

        public static SpellId? TryParseSpellId(string spellIdStr)
        {
            if (SpellNames == null)
                BuildSpellNames();

            var number = TryParseNumbers(spellIdStr, out _);
            if (number != null)
                spellIdStr = spellIdStr.Replace(",", "");
            
            if (!Enum.TryParse(spellIdStr, true, out SpellId spellId))
            {
                if (!SpellNames.TryGetValue(spellIdStr, out var spellName))
                {
                    Console.WriteLine($"TryParseSpellId() - couldn't convert SpellId from \"{spellIdStr}\"");
                    return null;
                }
                spellId = (SpellId)spellName;
            }
            return spellId;
        }

        public static PlayScript? TryParsePlayScript(string playScriptStr)
        {
            playScriptStr = playScriptStr.Replace("PlayScript.", "");
            playScriptStr = playScriptStr.Replace("PhysScript.", "");
            playScriptStr = playScriptStr.Replace("PScript.", "");

            if (!Enum.TryParse(playScriptStr, true, out PlayScript playScript))
            {
                if (playScriptStr.StartsWith("0x") && uint.TryParse(playScriptStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var pScriptNum))
                {
                    return (PlayScript)pScriptNum;
                }
                else
                {
                    Console.WriteLine($"TryParsePlayScript() - couldn't convert PlayScript from \"{playScriptStr}\"");
                    return null;
                }
            }
            return playScript;
        }

        public static Sound? TryParseSound(string soundStr)
        {
            soundStr = soundStr.Replace("Sound.", "");
            
            if (!Enum.TryParse(soundStr, true, out Sound sound))
            {
                if (soundStr.StartsWith("0x") && uint.TryParse(soundStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var soundNum))
                {
                    return (Sound)soundNum;
                }
                else
                {
                    Console.WriteLine($"TryParseSound() - couldn't convert Sound from \"{soundStr}\"");
                    return null;
                }
            }
            return sound;
        }

        public static DestinationType? TryParseDestinationType(string destinationTypeStr)
        {
            destinationTypeStr = destinationTypeStr.Replace("DestinationType.", "");
            destinationTypeStr = destinationTypeStr.Replace("Destination.", "");

            if (!Enum.TryParse(destinationTypeStr, true, out DestinationType destinationType))
            {
                Console.WriteLine($"TryParseDestinationType() - couldn't parse DestinationType from \"{destinationTypeStr}\"");
                return null;
            }
            return destinationType;
        }

        public static uint? TryParseUInt(string uintStr, bool showError = true)
        {
            uintStr = uintStr.Replace(",", "");

            uint uintVal = 0;
            
            if (uintStr.StartsWith("0x"))
            {
                if (!uint.TryParse(uintStr.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uintVal))
                {
                    if (showError)
                        Console.WriteLine($"TryParseUInt() - couldn't parse uint from \"{uintStr}\"");
                    return null;
                }
                return uintVal;
            }
            
            if (!uint.TryParse(uintStr, out uintVal))
            {
                if (showError)
                    Console.WriteLine($"TryParseUInt() - couldn't parse uint from \"{uintStr}\"");
                return null;
            }
            return uintVal;
        }

        public static VendorType? TryParseVendorType(string vendorTypeStr)
        {
            vendorTypeStr = vendorTypeStr.Replace("VendorType.", "");
            vendorTypeStr = vendorTypeStr.Replace("Vendor.", "");

            if (!Enum.TryParse(vendorTypeStr, true, out VendorType vendorType))
            {
                Console.WriteLine($"TryParseVendorType() - couldn't convert VendorType from \"{vendorTypeStr}\"");
                return null;
            }
            return vendorType;
        }

        public static CharacterTitle? TryParseCharacterTitle(string characterTitleStr)
        {
            characterTitleStr = characterTitleStr.Replace("CharacterTitle.", "");
            characterTitleStr = characterTitleStr.Replace("Title.", "");

            if (!Enum.TryParse(characterTitleStr, true, out CharacterTitle characterTitle))
            {
                Console.WriteLine($"TryParseCharacterTitle() - couldn't convert CharacterTitle from \"{characterTitleStr}\"");
                return null;
            }
            return characterTitle;
        }

        public static uint? TryParseWeenieClassId(string weenieClassIdStr)
        {
            var wcid = TryParseUInt(ParseToken(weenieClassIdStr), false);
            if (wcid != null)
                return wcid;

            var match = Regex.Match(weenieClassIdStr, @"\((\d+)\)");
            if (!match.Success)
            {
                Console.WriteLine($"TryParseWeenieClassId() - couldn't convert WeenieClassId from \"{weenieClassIdStr}\"");
                return null;
            }

            wcid = TryParseUInt(match.Groups[1].Value);
            if (wcid == null)
            {
                Console.WriteLine($"TryParseWeenieClassId() - couldn't convert WeenieClassId from \"{weenieClassIdStr}\"");
                return null;
            }

            return wcid;
        }

        public static PropertyAttribute? TryParsePropertyAttribute(string propAttrStr)
        {
            propAttrStr = propAttrStr.Replace("PropertyAttribute.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(propAttrStr, true, out PropertyAttribute propAttr))
            {
                Console.WriteLine($"TryParsePropertyAttribute() - couldn't convert PropertyAttribute from \"{propAttrStr}\"");
                return null;
            }
            return propAttr;
        }

        public static PropertyAttribute2nd? TryParsePropertyAttribute2nd(string propVitalStr)
        {
            propVitalStr = propVitalStr.Replace("PropertyAttribute2nd.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(propVitalStr, true, out PropertyAttribute2nd propVital))
            {
                Console.WriteLine($"TryParsePropertyAttribute2nd() - couldn't convert PropertyAttribute2nd from \"{propVitalStr}\"");
                return null;
            }
            return propVital;
        }

        public static PropertyBool? TryParsePropertyBool(string propBoolStr)
        {
            propBoolStr = propBoolStr.Replace("PropertyBool.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(propBoolStr, true, out PropertyBool propBool))
            {
                Console.WriteLine($"TryParsePropertyBool() - couldn't convert PropertyBool from \"{propBoolStr}\"");
                return null;
            }
            return propBool;
        }

        public static PropertyFloat? TryParsePropertyFloat(string propFloatStr)
        {
            propFloatStr = propFloatStr.Replace("PropertyFloat.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(propFloatStr, true, out PropertyFloat propFloat))
            {
                Console.WriteLine($"TryParsePropertyFloat() - couldn't convert PropertyFloat from \"{propFloatStr}\"");
                return null;
            }
            return propFloat;
        }

        public static PropertyInt? TryParsePropertyInt(string propIntStr)
        {
            propIntStr = propIntStr.Replace("PropertyInt.", "", StringComparison.OrdinalIgnoreCase);
            
            if (!Enum.TryParse(propIntStr, true, out PropertyInt propInt))
            {
                Console.WriteLine($"TryParsePropertyInt() - couldn't convert PropertyInt from \"{propIntStr}\"");
                return null;
            }
            return propInt;
        }

        public static PropertyInt64? TryParsePropertyInt64(string propInt64Str)
        {
            propInt64Str = propInt64Str.Replace("PropertyInt64.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(propInt64Str, true, out PropertyInt64 propInt64))
            {
                Console.WriteLine($"TryParsePropertyInt64() - couldn't convert PropertyInt64 from \"{propInt64Str}\"");
                return null;
            }
            return propInt64;
        }

        public static PropertyString? TryParsePropertyString(string propStrStr)
        {
            propStrStr = propStrStr.Replace("PropertyString.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(propStrStr, true, out PropertyString propStr))
            {
                Console.WriteLine($"TryParsePropertyString() - couldn't convert PropertyString from \"{propStrStr}\"");
                return null;
            }
            return propStr;
        }

        public static ContractId? TryParseContractId(string contractIdStr)
        {
            contractIdStr = contractIdStr.Replace("ContractId.", "", StringComparison.OrdinalIgnoreCase);
            contractIdStr = contractIdStr.Replace("Contract.", "", StringComparison.OrdinalIgnoreCase);

            if (!Enum.TryParse(contractIdStr, true, out ContractId contractId))
            {
                Console.WriteLine($"TryParseContractId() - couldn't convert ContractId from \"{contractIdStr}\"");
                return null;
            }
            return contractId;
        }

        public static string ParseToken(string line)
        {
            var idx = line.IndexOf(' ');
            if (idx != -1)
                return line.Substring(0, idx);
            else
                return line;
        }

        public static string TryParseLetters(string line, out string next)
        {
            next = line;
            
            var match = Regex.Match(line, @"^([A-Za-z ]+)");
            if (!match.Success)
                return null;

            var letters = match.Groups[1].Value;
            next = line.Substring(letters.Length);

            return letters.Trim();
        }

        public static int? TryParseNumbers(string line, out string next)
        {
            next = line;

            var match = Regex.Match(line, @"^([0-9, -]+)");
            if (!match.Success)
                return null;

            var numbersStr = match.Groups[1].Value;

            var numbers = TryParseInt(numbersStr);
            if (numbers == null)
                return null;

            next = line.Substring(numbersStr.Length);

            return numbers.Value;
        }

        public static long? TryParseNumbersLong(string line, out string next)
        {
            next = line;

            var match = Regex.Match(line, @"^([0-9, -]+)");
            if (!match.Success)
                return null;

            var numbersStr = match.Groups[1].Value;

            var numbers = TryParseInt64(numbersStr);
            if (numbers == null)
                return null;

            next = line.Substring(numbersStr.Length);

            return numbers.Value;
        }

        public static float? TryParsePercent(string line, out string next)
        {
            next = line;

            var match = Regex.Match(line, @"^([0-9%. E+-]+)");
            if (!match.Success)
                return null;

            var percentStr = match.Groups[1].Value.Trim();
            
            var isPercent = percentStr.Contains('%');

            if (isPercent)
                percentStr = percentStr.Replace("%", "");

            var percent = TryParseFloat(percentStr);

            if (percent != null)
            {
                next = line.Substring(match.Groups[1].Value.Length);
                return isPercent ? percent * 0.01f : percent;
            }
            return null;
        }

        public static Range TryParseRange(string line, RangeType rangeType, out string next)
        {
            next = line;

            var match = Regex.Match(line, @"^([0-9, -]+) ?- ?([0-9, -]+)");
            if (!match.Success)
            {
                var numbers = TryParseNumbers(line, out next);
                if (numbers != null)
                    return rangeType == RangeType.Min ? new Range(numbers, null) : new Range(null, numbers);
                else
                    return null;
            }

            var minStr = match.Groups[1].Value.Trim();
            var maxStr = match.Groups[2].Value.Trim();

            var min = TryParseInt(minStr);
            var max = TryParseInt(maxStr);

            if (min == null || max == null)
                return null;

            next = line.Substring(match.Groups[0].Value.Length).Trim();
            
            return new Range(min, max);
        }

        public static Range64 TryParseRange64(string line, RangeType rangeType, out string next)
        {
            next = line;

            var match = Regex.Match(line, @"^([0-9, -]+) ?- ?([0-9, -]+)");
            if (!match.Success)
            {
                var numbers = TryParseNumbersLong(line, out next);
                if (numbers != null)
                    return rangeType == RangeType.Min ? new Range64(numbers, null) : new Range64(null, numbers);
                else
                    return null;
            }

            var minStr = match.Groups[1].Value.Trim();
            var maxStr = match.Groups[2].Value.Trim();

            var min = TryParseInt64(minStr);
            var max = TryParseInt64(maxStr);

            if (min == null || max == null)
                return null;

            next = line.Substring(match.Groups[0].Value.Length).Trim();

            return new Range64(min, max);
        }

        public static RangeFloat TryParseRangeFloat(string line, RangeType rangeType, out string next)
        {
            next = line;

            var match = Regex.Match(line, @"^([0-9.%, E+-]+) ?- ?([0-9.%, E+-]+)");
            if (!match.Success)
            {
                var numbers = TryParsePercent(line, out next);
                if (numbers != null)
                    return rangeType == RangeType.Min ? new RangeFloat(numbers, null) : new RangeFloat(null, numbers);
                else
                    return null;
            }

            var minStr = match.Groups[1].Value.Trim();
            var maxStr = match.Groups[2].Value.Trim();

            var min = TryParsePercent(minStr, out _);
            var max = TryParsePercent(maxStr, out _);

            if (min == null || max == null)
                return null;

            next = line.Substring(match.Groups[0].Value.Length).Trim();

            return new RangeFloat(min, max);
        }

        public static Skill? TryParseSkill(string skillStr)
        {
            skillStr = skillStr.Replace(" ", "");
            
            if (!Enum.TryParse(skillStr, true, out Skill skill))
            {
                Console.WriteLine($"TryParseSkill() - couldn't convert Skill from \"{skillStr}\"");
                return null;
            }
            return skill;
        }

        public static Position TryParsePosition(string positionStr)
        {
            var match = Regex.Match(positionStr, @"0x([0-9A-F]{8}) \[([\d.-]+) ([\d.-]+) ([\d.-]+)\] ([\d.-]+) ([\d.-]+) ([\d.-]+) ([\d.-]+)", RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                Console.WriteLine($"TryParsePosition() - couldn't convert Position from \"{positionStr}\"");
                return null;
            }
            if (!uint.TryParse(match.Groups[1].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var objCellId))
            {
                Console.WriteLine($"TryParsePosition() - couldn't convert Position.ObjCellId from \"{positionStr}\"");
                return null;
            }
            if (!float.TryParse(match.Groups[2].Value, out var originX) || !float.TryParse(match.Groups[3].Value, out var originY) || !float.TryParse(match.Groups[4].Value, out var originZ))
            {
                Console.WriteLine($"TryParsePosition() - couldn't convert Position.Origin from \"{positionStr}\"");
                return null;
            }
            if (!float.TryParse(match.Groups[5].Value, out var anglesW) || !float.TryParse(match.Groups[6].Value, out var anglesX) || !float.TryParse(match.Groups[7].Value, out var anglesY) || !float.TryParse(match.Groups[8].Value, out var anglesZ))
            {
                Console.WriteLine($"TryParsePosition() - couldn't convert Position.Orientation from \"{positionStr}\"");
                return null;
            }
            var position = new Position(objCellId, new Vector3(originX, originY, originZ), new Quaternion(anglesX, anglesY, anglesZ, anglesW));
            
            return position;
        }

        public static Frame TryParseFrame(string frameStr, bool showError = true)
        {
            var match = Regex.Match(frameStr, @"\[([\d.-]+) ([\d.-]+) ([\d.-]+)\] ([\d.-]+) ([\d.-]+) ([\d.-]+) ([\d.-]+)");

            if (!match.Success)
            {
                if (showError)
                    Console.WriteLine($"TryParseFrame() - couldn't convert Frame from \"{frameStr}\"");
                return null;
            }
            if (!float.TryParse(match.Groups[1].Value, out var originX) || !float.TryParse(match.Groups[2].Value, out var originY) || !float.TryParse(match.Groups[3].Value, out var originZ))
            {
                if (showError)
                    Console.WriteLine($"TryParseFrame() - couldn't convert Frame.Origin from \"{frameStr}\"");
                return null;
            }
            if (!float.TryParse(match.Groups[4].Value, out var anglesW) || !float.TryParse(match.Groups[5].Value, out var anglesX) || !float.TryParse(match.Groups[6].Value, out var anglesY) || !float.TryParse(match.Groups[7].Value, out var anglesZ))
            {
                if (showError)
                    Console.WriteLine($"TryParseFrame() - couldn't convert Frame.Orientation from \"{frameStr}\"");
                return null;
            }
            var frame = new Frame(new Vector3(originX, originY, originZ), new Quaternion(anglesX, anglesY, anglesZ, anglesW));

            return frame;
        }

        public static Vector3? TryParseVector3(string vectorStr)
        {
            var match = Regex.Match(vectorStr, @"([\d.-]+) ([\d.-]+) ([\d.-]+)");

            if (!match.Success)
            {
                Console.WriteLine($"TryParsePosition() - couldn't convert Vector3 from \"{vectorStr}\"");
                return null;
            }
            if (!float.TryParse(match.Groups[1].Value, out var originX) || !float.TryParse(match.Groups[2].Value, out var originY) || !float.TryParse(match.Groups[3].Value, out var originZ))
            {
                Console.WriteLine($"TryParsePosition() - couldn't convert Vector3 from \"{vectorStr}\"");
                return null;
            }

            var pos = new Vector3(originX, originY, originZ);

            return pos;
        }

        public static float DegreesToRads(float degrees)
        {
            return degrees * (float)Math.PI / 180.0f;
        }

        public static Dictionary<string, float> Dirs = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase)
        {
            { "N",         0 },
            { "NW",        45 },
            { "W",         90 },
            { "SW",        135 },
            { "S",         180 },
            { "SE",        225 },
            { "E",         270 },
            { "NE",        315 },

            { "North",     0 },
            { "NorthWest", 45 },
            { "West",      90 },
            { "SouthWest", 135 },
            { "South",     180 },
            { "SouthEast", 225 },
            { "East",      270 },
            { "NorthEast", 315 },
        };

        public static Quaternion? TryParseQuaternion(string rotStr)
        {
            if (float.TryParse(rotStr, out var degrees) || Dirs.TryGetValue(rotStr, out degrees))
            {
                // heading
                var rads = DegreesToRads(degrees);
                return Quaternion.CreateFromAxisAngle(Vector3.UnitY, rads);
            }
            
            var match = Regex.Match(rotStr, @"([\d.-]+) ([\d.-]+) ([\d.-]+) ([\d.-]+)");

            if (!match.Success)
            {
                Console.WriteLine($"TryParseRotation() - couldn't convert Rotation from \"{rotStr}\"");
                return null;
            }
            if (!float.TryParse(match.Groups[1].Value, out var anglesW) || !float.TryParse(match.Groups[2].Value, out var anglesX) || !float.TryParse(match.Groups[3].Value, out var anglesY) || !float.TryParse(match.Groups[4].Value, out var anglesZ))
            {
                Console.WriteLine($"TryParseRotation() - couldn't convert Rotation from \"{rotStr}\"");
                return null;
            }

            var rotate = new Quaternion(anglesX, anglesY, anglesZ, anglesW);

            // verify normalization?

            return rotate;
        }

        public static void BuildSpellNames()
        {
            SpellNames = Reader.GetNameToIDs(Reader.GetIDToNames("SpellName.txt"));
        }
    }
}
