using System;
using System.Collections.Generic;

using EmoteScript.Entity.Enum;

namespace EmoteScript
{
    public class EmoteSet_Line: Line
    {
        public EmoteSet EmoteSet;
        
        public static Dictionary<EmoteSetField, FieldType> FieldTypes = new Dictionary<EmoteSetField, FieldType>()
        {
            { EmoteSetField.Probability,   FieldType.Float },
            { EmoteSetField.WeenieClassId, FieldType.UInt32 },
            { EmoteSetField.Style,         FieldType.MotionStance },
            { EmoteSetField.Substyle,      FieldType.MotionCommand },
            { EmoteSetField.Quest,         FieldType.String },
            { EmoteSetField.VendorType,    FieldType.VendorType },
            { EmoteSetField.MinHealth,     FieldType.Float },
            { EmoteSetField.MaxHealth,     FieldType.Float }
        };

        public EmoteSet_Line(EmoteCategory category, Dictionary<string, string> dict)
        {
            EmoteSet = BuildEmoteSet(category, dict);
        }

        public static EmoteSet BuildEmoteSet(EmoteCategory category, Dictionary<string, string> dict)
        {
            var emoteSet = new EmoteSet(category);

            var kvps = ParseFieldDictionary(category, dict);

            foreach (var kvp in kvps)
            {
                if (!FieldTypes.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"EmoteSet_Line.BuldEmote({kvp.Key}) - skipping");
                    continue;
                }

                var val = kvp.Value;

                switch (kvp.Key)
                {
                    case EmoteSetField.Probability:
                        emoteSet.Probability = Parser.TryParsePercent(val, out _);
                        break;

                    case EmoteSetField.WeenieClassId:
                        emoteSet.WeenieClassId = Parser.TryParseUInt(val);
                        break;

                    case EmoteSetField.Style:
                        emoteSet.Style = Parser.TryParseMotionStance(val);
                        break;

                    case EmoteSetField.Substyle:
                        emoteSet.Substyle = Parser.TryParseMotionCommand(val);
                        break;

                    case EmoteSetField.Quest:
                        emoteSet.Quest = val;
                        break;

                    case EmoteSetField.VendorType:
                        emoteSet.VendorType = Parser.TryParseVendorType(val);
                        break;

                    case EmoteSetField.MinHealth:
                        emoteSet.MinHealth = Parser.TryParsePercent(val, out _);
                        break;

                    case EmoteSetField.MaxHealth:
                        emoteSet.MaxHealth = Parser.TryParsePercent(val, out _);
                        break;
                }
            }

            return emoteSet;
        }

        public static Dictionary<EmoteSetField, string> ParseFieldDictionary(EmoteCategory category, Dictionary<string, string> dict)
        {
            var fieldDict = new Dictionary<EmoteSetField, string>();

            foreach (var kvp in dict)
            {
                var key = kvp.Key;
                var val = kvp.Value;

                if (key.Equals(string.Empty))
                {
                    ParseOptionalFields(category, val, fieldDict);
                    continue;
                }

                var field = GetField(key);

                if (field == null)
                {
                    Console.WriteLine($"EmoteSet_Line.ParseFieldDictionary: unknown emote set field \"{key}\"");
                    continue;
                }

                fieldDict.Add(field.Value, val);
            }
            return fieldDict;
        }

        public static EmoteSetField? GetField(string fieldName)
        {
            if (!Enum.TryParse(fieldName, true, out EmoteSetField field))
            {
                Console.WriteLine($"EmoteSet_Line.GetField({fieldName}): unknown emote set field");
                return null;
            }
            return field;
        }

        public static List<string> GetSubTokens(string line)
        {
            var split = line.Split(", ");

            var tokens = new List<string>();
            foreach (var item in split)
            {
                var equals = item.Split(" = ");

                foreach (var equal in equals)
                {
                    var trimmed = equal.Trim();
                    if (trimmed.Length == 0)
                        continue;

                    tokens.Add(trimmed);
                }
            }
            return tokens;
        }

        public static void ParseOptionalFields(EmoteCategory category, string fields, Dictionary<EmoteSetField, string> fieldDict)
        {
            DefaultFields.TryGetValue(category, out var defaultFields);

            if (defaultFields == null)
            {
                Console.WriteLine($"EmoteSet_Line.ParseOptionalFields({category}): couldn't find default field for {category}");
                return;
            }

            // custom parsers
            var tokens = new List<string>() { fields };
            if (defaultFields.Count > 1)
            {
                tokens = GetSubTokens(fields);

                if (tokens.Count > defaultFields.Count)
                    Console.WriteLine($"Warning: EmoteSet_LineParseOptionalFields({category}): {fields} contains more than default # of fields");
            }

            for (var i = 0; i < tokens.Count && i < defaultFields.Count; i++)
            {
                var token = tokens[i];
                var defaultField = defaultFields[i];

                switch (defaultField)
                {
                    case EmoteSetField.WeenieClassId:

                        var wcid = Parser.TryParseWeenieClassId(token);
                        if (wcid != null)
                            fieldDict.Add(EmoteSetField.WeenieClassId, wcid.ToString());

                        break;

                    case EmoteSetField.VendorType:

                        var vendorType = Parser.TryParseVendorType(token);
                        if (vendorType != null)
                            fieldDict.Add(EmoteSetField.VendorType, ((int)vendorType).ToString());

                        break;

                    default:
                        fieldDict.Add(defaultField, token);
                        break;
                }
            }
        }

        public static Dictionary<EmoteCategory, List<EmoteSetField>> DefaultFields = new Dictionary<EmoteCategory, List<EmoteSetField>>()
        {
            { EmoteCategory.EventFailure, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.EventSuccess, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.Give, new List<EmoteSetField>() { EmoteSetField.WeenieClassId } },
            { EmoteCategory.GotoSet, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.NumCharacterTitlesFailure, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.NumCharacterTitlesSuccess, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.NumFellowsFailure, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.NumFellowsSuccess, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.ReceiveLocalSignal, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.Refuse, new List<EmoteSetField>() { EmoteSetField.WeenieClassId } },
            { EmoteCategory.QuestFailure, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.QuestNoFellow, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.QuestSuccess, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.TestFailure, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.TestSuccess, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.TestNoFellow, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.TestNoQuality, new List<EmoteSetField>() { EmoteSetField.Quest } },
            { EmoteCategory.Vendor, new List<EmoteSetField>() { EmoteSetField.VendorType } },
        };
    }
}
