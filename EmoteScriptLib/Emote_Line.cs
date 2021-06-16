using System;
using System.Collections.Generic;
using System.Numerics;

using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib
{
    public class Emote_Line : Line
    {
        public Emote Emote;
        
        public static Dictionary<EmoteField, FieldType> FieldTypes = new Dictionary<EmoteField, FieldType>()
        {
            { EmoteField.Delay,           FieldType.Float },
            { EmoteField.Extent,          FieldType.Float },
            { EmoteField.Motion,          FieldType.MotionCommand },
            { EmoteField.Message,         FieldType.String },
            { EmoteField.TestString,      FieldType.String },
            { EmoteField.Min,             FieldType.Int32 },
            { EmoteField.Max,             FieldType.Int32 },
            { EmoteField.Min64,           FieldType.Int64 },
            { EmoteField.Max64,           FieldType.Int64 },
            { EmoteField.MinFloat,        FieldType.Float },
            { EmoteField.MaxFloat,        FieldType.Float },
            { EmoteField.Stat,            FieldType.Int32 },
            { EmoteField.Display,         FieldType.Bool },
            { EmoteField.Amount,          FieldType.Int32 },
            { EmoteField.Amount64,        FieldType.Int64 },
            { EmoteField.HeroXP64,        FieldType.Int64 },
            { EmoteField.Percent,         FieldType.Double },
            { EmoteField.SpellId,         FieldType.SpellId },
            { EmoteField.WealthRating,    FieldType.Int32 },
            { EmoteField.TreasureClass,   FieldType.Int32 },
            { EmoteField.TreasureType,    FieldType.Int32 },
            { EmoteField.PScript,         FieldType.PlayScript },
            { EmoteField.Sound,           FieldType.Sound },
            { EmoteField.DestinationType, FieldType.DestinationType },
            { EmoteField.WeenieClassId,   FieldType.UInt32 },
            { EmoteField.StackSize,       FieldType.Int32 },
            { EmoteField.Palette,         FieldType.Int32 },
            { EmoteField.Shade,           FieldType.Float },
            { EmoteField.TryToBond,       FieldType.Bool },
            { EmoteField.ObjCellId,       FieldType.UInt32 },
            { EmoteField.OriginX,         FieldType.Float },
            { EmoteField.OriginY,         FieldType.Float },
            { EmoteField.OriginZ,         FieldType.Float },
            { EmoteField.AnglesW,         FieldType.Float },
            { EmoteField.AnglesX,         FieldType.Float },
            { EmoteField.AnglesY,         FieldType.Float },
            { EmoteField.AnglesZ,         FieldType.Float },
        };

        public Emote_Line(EmoteType type, Dictionary<string, string> dict, float? delay = null)
        {
            Emote = BuildEmote(type, dict, delay);
        }

        public static Emote BuildEmote(EmoteType type, Dictionary<string, string> dict, float? delay = null)
        {
            var emote = Factory.Create(type);

            emote.Delay = delay;

            var kvps = ParseFieldDictionary(type, dict);

            foreach (var kvp in kvps)
            {
                if (!FieldTypes.ContainsKey(kvp.Key))
                {
                    Console.WriteLine($"Emote_Line.BuildEmote({kvp.Key}) - skipping");
                    continue;
                }

                var val = kvp.Value;
                
                switch (kvp.Key)
                {
                    case EmoteField.Delay:
                        emote.Delay = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.Extent:
                        emote.Extent = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.Motion:
                        emote.Motion = Parser.TryParseMotionCommand(val);
                        break;

                    case EmoteField.Message:
                        emote.Message = val;
                        break;

                    case EmoteField.TestString:
                        emote.TestString = val;
                        break;

                    case EmoteField.Min:
                        emote.Min = Parser.TryParseInt(val);
                        break;

                    case EmoteField.Max:
                        emote.Max = Parser.TryParseInt(val);
                        break;

                    case EmoteField.Min64:
                        emote.Min64 = Parser.TryParseInt64(val);
                        break;

                    case EmoteField.Max64:
                        emote.Max64 = Parser.TryParseInt64(val);
                        break;

                    case EmoteField.MinFloat:
                        emote.MinFloat = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.MaxFloat:
                        emote.MaxFloat = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.Stat:
                        emote.Stat = Parser.TryParseInt(val);
                        break;

                    case EmoteField.Display:
                        emote.Display = Parser.TryParseBool(val);
                        break;

                    case EmoteField.Amount:
                        emote.Amount = Parser.TryParseInt(val);
                        break;

                    case EmoteField.Amount64:
                        emote.Amount64 = Parser.TryParseInt64(val);
                        break;

                    case EmoteField.HeroXP64:
                        emote.HeroXP64 = Parser.TryParseInt64(val);
                        break;

                    case EmoteField.Percent:
                        emote.Percent = Parser.TryParseDouble(val);
                        break;

                    case EmoteField.SpellId:
                        emote.SpellId = Parser.TryParseSpellId(val);
                        break;

                    case EmoteField.WealthRating:
                        emote.WealthRating = Parser.TryParseInt(val);
                        break;

                    case EmoteField.TreasureClass:
                        emote.TreasureClass = Parser.TryParseInt(val);
                        break;

                    case EmoteField.TreasureType:
                        emote.TreasureType = Parser.TryParseInt(val);
                        break;

                    case EmoteField.PScript:
                        emote.PScript = Parser.TryParsePlayScript(val);
                        break;

                    case EmoteField.Sound:
                        emote.Sound = Parser.TryParseSound(val);
                        break;

                    case EmoteField.DestinationType:
                        emote.DestinationType = Parser.TryParseDestinationType(val);
                        break;

                    case EmoteField.WeenieClassId:
                        emote.WeenieClassId = Parser.TryParseUInt(val);
                        break;

                    case EmoteField.StackSize:
                        emote.StackSize = Parser.TryParseInt(val);
                        break;

                    case EmoteField.Palette:
                        emote.Palette = Parser.TryParseInt(val);
                        break;

                    case EmoteField.Shade:
                        emote.Shade = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.TryToBond:
                        emote.TryToBond = Parser.TryParseBool(val);
                        break;

                    case EmoteField.ObjCellId:
                        emote.ObjCellId = Parser.TryParseUInt(val);
                        break;

                    case EmoteField.OriginX:
                        emote.OriginX = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.OriginY:
                        emote.OriginY = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.OriginZ:
                        emote.OriginZ = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.AnglesW:
                        emote.AnglesW = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.AnglesX:
                        emote.AnglesX = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.AnglesY:
                        emote.AnglesY = Parser.TryParseFloat(val);
                        break;

                    case EmoteField.AnglesZ:
                        emote.AnglesZ = Parser.TryParseFloat(val);
                        break;
                }
            }

            emote.Message = emote.GetMessageKey();
            
            return emote;
        }

        public static Dictionary<EmoteField, string> ParseFieldDictionary(EmoteType type, Dictionary<string, string> dict)
        {
            var fieldDict = new Dictionary<EmoteField, string>();

            foreach (var kvp in dict)
            {
                var key = kvp.Key;
                var val = kvp.Value;

                if (key.Equals(string.Empty))
                {
                    ParseOptionalFields(type, val, fieldDict);
                    continue;
                }

                var field = GetField(key);

                if (field == null)
                {
                    Console.WriteLine($"Emote_Line.ParseFieldDictionary: unknown emote field \"{key}\"");
                    continue;
                }

                fieldDict.Add(field.Value, val);
            }
            return fieldDict;
        }

        public static EmoteField? GetField(string fieldName)
        {
            if (!Enum.TryParse(fieldName, true, out EmoteField field))
            {
                Console.WriteLine($"Emote_Line.GetField({fieldName}): unknown emote field");
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

        public static void ParseOptionalFields(EmoteType type, string fields, Dictionary<EmoteField, string> fieldDict)
        {
            DefaultFields.TryGetValue(type, out var defaultFields);

            if (defaultFields == null)
            {
                Console.WriteLine($"ParseOptionalFields({type}): couldn't find default field for {type}");
                return;
            }

            // custom parsers
            var tokens = new List<string>() { fields };
            if (defaultFields.Count > 1)
            {
                tokens = GetSubTokens(fields);

                if (tokens.Count > defaultFields.Count)
                    Console.WriteLine($"Warning: ParseOptionalFields({type}): {fields} contains more than default # of fields");
            }

            for (var i = 0; i < tokens.Count && i < defaultFields.Count; i++)
            {
                var token = tokens[i];
                var defaultField = defaultFields[i];

                switch (defaultField)
                {
                    case EmoteField.Amount:

                        var amount = token;

                        if (token.Equals("false", StringComparison.OrdinalIgnoreCase))
                            amount = "0";
                        else if (token.Equals("true", StringComparison.OrdinalIgnoreCase))
                            amount = "1";

                        fieldDict.Add(EmoteField.Amount, amount);
                        break;

                    case EmoteField.CharacterTitle:
                        var title = Parser.TryParseCharacterTitle(token);
                        if (title != null)
                            fieldDict.Add(EmoteField.Amount, ((int)title).ToString());
                        break;

                    case EmoteField.PropertyAttributeStat:
                        var propAttr = Parser.TryParsePropertyAttribute(token);
                        if (propAttr != null)
                            fieldDict.Add(EmoteField.Stat, ((int)propAttr).ToString());
                        break;

                    case EmoteField.PropertyAttribute2ndStat:
                        var propVital = Parser.TryParsePropertyAttribute2nd(token);
                        if (propVital != null)
                            fieldDict.Add(EmoteField.Stat, ((int)propVital).ToString());
                        break;

                    case EmoteField.PropertyBoolStat:
                        var propBool = Parser.TryParsePropertyBool(token);
                        if (propBool != null)
                            fieldDict.Add(EmoteField.Stat, ((int)propBool).ToString());
                        break;

                    case EmoteField.PropertyFloatStat:
                        var propFloat = Parser.TryParsePropertyFloat(token);
                        if (propFloat != null)
                            fieldDict.Add(EmoteField.Stat, ((int)propFloat).ToString());
                        break;

                    case EmoteField.PropertyIntStat:
                        var propInt = Parser.TryParsePropertyInt(token);
                        if (propInt != null)
                            fieldDict.Add(EmoteField.Stat, ((int)propInt).ToString());
                        break;

                    case EmoteField.PropertyInt64Stat:
                        var propInt64 = Parser.TryParsePropertyInt64(token);
                        if (propInt64 != null)
                            fieldDict.Add(EmoteField.Stat, ((int)propInt64).ToString());
                        break;

                    case EmoteField.PropertyStringStat:
                        var propStr = Parser.TryParsePropertyString(token);
                        if (propStr != null)
                            fieldDict.Add(EmoteField.Stat, ((int)propStr).ToString());
                        break;

                    case EmoteField.SkillStat:
                        var skill = Parser.TryParseSkill(token);
                        if (skill != null)
                            fieldDict.Add(EmoteField.Stat, ((int)skill).ToString());
                        break;

                    case EmoteField.Percent:
                        var percent = Parser.TryParsePercent(token, out _);
                        if (percent != null)
                            fieldDict.Add(EmoteField.Percent, percent.ToString());
                        break;

                    case EmoteField.Range:

                        if (!RangeTypes.TryGetValue(type, out var rangeType))
                        {
                            Console.WriteLine($"Emote_Line.ParseOptionalTypes({type}) couldn't find RangeType");
                            break;
                        }

                        var range = Parser.TryParseRange(token, rangeType, out _);
                        if (range != null)
                        {
                            if (range.Min != null)
                                fieldDict.Add(EmoteField.Min, range.Min.ToString());
                            if (range.Max != null)
                                fieldDict.Add(EmoteField.Max, range.Max.ToString());
                        }
                        break;

                    case EmoteField.RangeFloat:

                        if (!RangeTypes.TryGetValue(type, out rangeType))
                        {
                            Console.WriteLine($"Emote_Line.ParseOptionalTypes({type}) couldn't find RangeType");
                            break;
                        }

                        var rangeFloat = Parser.TryParseRangeFloat(token, rangeType, out _);
                        if (rangeFloat != null)
                        {
                            if (rangeFloat.Min != null)
                                fieldDict.Add(EmoteField.MinFloat, rangeFloat.Min.ToString());
                            if (rangeFloat.Max != null)
                                fieldDict.Add(EmoteField.MaxFloat, rangeFloat.Max.ToString());
                        }
                        break;

                    case EmoteField.Range64:


                        if (!RangeTypes.TryGetValue(type, out rangeType))
                        {
                            Console.WriteLine($"Emote_Line.ParseOptionalTypes({type}) couldn't find RangeType");
                            break;
                        }

                        var range64 = Parser.TryParseRange64(token, rangeType, out _);
                        if (range64 != null)
                        {
                            if (range64.Min != null)
                                fieldDict.Add(EmoteField.Min64, range64.Min.ToString());
                            if (range64.Max != null)
                                fieldDict.Add(EmoteField.Max64, range64.Max.ToString());
                        }
                        break;

                    case EmoteField.WeenieClassId:

                        var wcid = Parser.TryParseWeenieClassId(token);
                        if (wcid != null)
                            fieldDict.Add(EmoteField.WeenieClassId, wcid.ToString());

                        break;

                    case EmoteField.Angles:

                        var angles = Parser.TryParseQuaternion(token);
                        if (angles != null)
                            AddAngles(angles.Value, fieldDict);
                        break;

                    case EmoteField.OriginAngles:

                        var frame = Parser.TryParseFrame(token, false);
                        if (frame != null)
                        {
                            AddOrigin(frame.Origin, fieldDict);
                            AddAngles(frame.Orientation, fieldDict);
                        }
                        else
                        {
                            var origin = Parser.TryParseVector3(token);
                            if (origin != null)
                                AddOrigin(origin.Value, fieldDict);
                        }
                        break;

                    case EmoteField.Position:

                        var pos = Parser.TryParsePosition(token);
                        if (pos != null)
                        {
                            fieldDict.Add(EmoteField.ObjCellId, pos.ObjCellId.ToString());
                            AddOrigin(pos.Frame.Origin, fieldDict);
                            AddAngles(pos.Frame.Orientation, fieldDict);
                        }
                        break;

                    case EmoteField.PScript:

                        var physScript = Parser.TryParsePlayScript(token);
                        if (physScript != null)
                            fieldDict.Add(EmoteField.PScript, ((int)physScript).ToString());
                        break;

                    case EmoteField.ContractId:

                        var contractId = Parser.TryParseContractId(token);
                        if (contractId != null)
                            fieldDict.Add(EmoteField.Stat, ((int)contractId).ToString());
                        break;

                    default:
                        fieldDict.Add(defaultField, token);
                        break;
                }
            }
        }

        public static void AddOrigin(Vector3 v, Dictionary<EmoteField, string> fieldDict)
        {
            fieldDict.Add(EmoteField.OriginX, v.X.ToString());
            fieldDict.Add(EmoteField.OriginY, v.Y.ToString());
            fieldDict.Add(EmoteField.OriginZ, v.Z.ToString());
        }

        public static void AddAngles(Quaternion q, Dictionary<EmoteField, string> fieldDict)
        {
            fieldDict.Add(EmoteField.AnglesW, q.W.ToString());
            fieldDict.Add(EmoteField.AnglesX, q.X.ToString());
            fieldDict.Add(EmoteField.AnglesY, q.Y.ToString());
            fieldDict.Add(EmoteField.AnglesZ, q.Z.ToString());
        }

        // the award types are RangeType.Max,
        // and the Inq* types are RangeType.Min
        public static Dictionary<EmoteType, RangeType> RangeTypes = new Dictionary<EmoteType, RangeType>()
        {
            { EmoteType.AwardLevelProportionalSkillXP, RangeType.Max },
            { EmoteType.AwardLevelProportionalXP,      RangeType.Max },

            { EmoteType.InqAttributeStat,              RangeType.Min },
            { EmoteType.InqFloatStat,                  RangeType.Min },
            { EmoteType.InqIntStat,                    RangeType.Min },
            { EmoteType.InqInt64Stat,                  RangeType.Min },
            { EmoteType.InqMyQuestSolves,              RangeType.Min },
            { EmoteType.InqQuestSolves,                RangeType.Min },
            { EmoteType.InqRawAttributeStat,           RangeType.Min },
            { EmoteType.InqRawSecondaryAttributeStat,  RangeType.Min },
            { EmoteType.InqRawSkillStat,               RangeType.Min },
            { EmoteType.InqSecondaryAttributeStat,     RangeType.Min },
            { EmoteType.InqSkillStat,                  RangeType.Min },
        };
        
        public static Dictionary<EmoteType, List<EmoteField>> DefaultFields = new Dictionary<EmoteType, List<EmoteField>>()
        {
            { EmoteType.Act, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.AddCharacterTitle, new List<EmoteField>() { EmoteField.CharacterTitle } },
            { EmoteType.AddContract, new List<EmoteField>() { EmoteField.ContractId } },
            { EmoteType.AdminSpam, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.AwardLevelProportionalSkillXP, new List<EmoteField>() { EmoteField.SkillStat, EmoteField.Percent, EmoteField.Range64 } },
            { EmoteType.AwardLevelProportionalXP, new List<EmoteField>() { EmoteField.Percent, EmoteField.Range64, EmoteField.Display } },
            { EmoteType.AwardLuminance, new List<EmoteField>() { EmoteField.HeroXP64 } },
            { EmoteType.AwardNoShareXP, new List<EmoteField>() { EmoteField.Amount64 } },
            { EmoteType.AwardSkillPoints, new List<EmoteField>() { EmoteField.SkillStat, EmoteField.Amount } },
            { EmoteType.AwardSkillXP, new List<EmoteField>() { EmoteField.SkillStat, EmoteField.Amount } },
            { EmoteType.AwardTrainingCredits, new List<EmoteField>() { EmoteField.Amount } },
            { EmoteType.AwardXP, new List<EmoteField>() { EmoteField.Amount64 } },
            { EmoteType.CastSpell, new List<EmoteField>() { EmoteField.SpellId } },
            { EmoteType.CastSpellInstant, new List<EmoteField>() { EmoteField.SpellId } },
            { EmoteType.CreateTreasure, new List<EmoteField>() { EmoteField.WealthRating } },
            { EmoteType.DecrementIntStat, new List<EmoteField>() { EmoteField.PropertyIntStat, EmoteField.Amount } },
            { EmoteType.DecrementMyQuest, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.DecrementQuest, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.DirectBroadcast, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.EraseMyQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.EraseQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.FellowBroadcast, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.ForceMotion, new List<EmoteField>() { EmoteField.Motion } },
            { EmoteType.Give, new List<EmoteField>() { EmoteField.WeenieClassId, EmoteField.StackSize } },
            { EmoteType.Goto, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.IncrementIntStat, new List<EmoteField>() { EmoteField.PropertyIntStat, EmoteField.Amount } },
            { EmoteType.IncrementMyQuest, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.IncrementQuest, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.InflictVitaePenalty, new List<EmoteField>() { EmoteField.Amount } },
            { EmoteType.InqAttributeStat, new List<EmoteField>() { EmoteField.PropertyAttributeStat, EmoteField.Range, EmoteField.Message } },
            { EmoteType.InqBoolStat, new List<EmoteField>() { EmoteField.PropertyBoolStat, EmoteField.Message } },
            { EmoteType.InqEvent, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.InqFellowNum, new List<EmoteField>() { EmoteField.Range, EmoteField.Message} },
            { EmoteType.InqFellowQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.InqFloatStat, new List<EmoteField>() { EmoteField.PropertyFloatStat, EmoteField.RangeFloat, EmoteField.Message } },
            { EmoteType.InqIntStat, new List<EmoteField>() { EmoteField.PropertyIntStat, EmoteField.Range, EmoteField.Message } },
            { EmoteType.InqInt64Stat, new List<EmoteField>() { EmoteField.PropertyInt64Stat, EmoteField.Range64, EmoteField.Message } },
            { EmoteType.InqMyQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.InqMyQuestBitsOff, new List<EmoteField>() {EmoteField.Message, EmoteField.Amount} },
            { EmoteType.InqMyQuestBitsOn, new List<EmoteField>() {EmoteField.Message, EmoteField.Amount} },
            { EmoteType.InqMyQuestSolves, new List<EmoteField>() { EmoteField.Message, EmoteField.Range } },
            { EmoteType.InqPackSpace, new List<EmoteField>() { EmoteField.Amount, EmoteField.Message } },
            { EmoteType.InqQuestBitsOff, new List<EmoteField>() {EmoteField.Message, EmoteField.Amount} },
            { EmoteType.InqQuestBitsOn, new List<EmoteField>() {EmoteField.Message, EmoteField.Amount} },
            { EmoteType.InqQuestSolves, new List<EmoteField>() { EmoteField.Message, EmoteField.Range} },
            { EmoteType.InqOwnsItems, new List<EmoteField>() { EmoteField.WeenieClassId, EmoteField.StackSize, EmoteField.Message } },
            { EmoteType.InqRawAttributeStat, new List<EmoteField>() { EmoteField.PropertyAttributeStat, EmoteField.Range, EmoteField.Message } },
            { EmoteType.InqRawSecondaryAttributeStat, new List<EmoteField>() { EmoteField.PropertyAttribute2ndStat, EmoteField.Range, EmoteField.Message } },
            { EmoteType.InqRawSkillStat, new List<EmoteField>() { EmoteField.SkillStat, EmoteField.Range, EmoteField.Message } },
            { EmoteType.InqSecondaryAttributeStat, new List<EmoteField>() { EmoteField.PropertyAttribute2ndStat, EmoteField.Range, EmoteField.Message } },
            { EmoteType.InqSkillSpecialized, new List<EmoteField>() { EmoteField.SkillStat, EmoteField.Message } },
            { EmoteType.InqSkillStat, new List<EmoteField>() { EmoteField.SkillStat, EmoteField.Range, EmoteField.Message } },
            { EmoteType.InqSkillTrained, new List<EmoteField>() { EmoteField.SkillStat, EmoteField.Message } },
            { EmoteType.InqStringStat, new List<EmoteField>() { EmoteField.PropertyStringStat, EmoteField.TestString, EmoteField.Message } },
            { EmoteType.InqQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.InqYesNo, new List<EmoteField>() { EmoteField.TestString, EmoteField.Message } },
            { EmoteType.LocalBroadcast, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.LocalSignal, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.Motion, new List<EmoteField>() { EmoteField.Motion } },
            { EmoteType.Move, new List<EmoteField>() { EmoteField.OriginAngles } },
            { EmoteType.MoveToPos, new List<EmoteField>() { EmoteField.Position } },
            { EmoteType.PetCastSpellOnOwner, new List<EmoteField>() { EmoteField.SpellId } },
            { EmoteType.PhysScript, new List<EmoteField>() { EmoteField.PScript } },
            { EmoteType.PopUp, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.RemoveContract, new List<EmoteField>() { EmoteField.ContractId } },
            { EmoteType.Say, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.SetBoolStat, new List<EmoteField>() { EmoteField.PropertyBoolStat, EmoteField.Amount } },
            { EmoteType.SetFloatStat, new List<EmoteField>() { EmoteField.PropertyFloatStat, EmoteField.Percent } },
            { EmoteType.SetIntStat, new List<EmoteField>() { EmoteField.PropertyIntStat, EmoteField.Amount } },
            { EmoteType.SetInt64Stat, new List<EmoteField>() { EmoteField.PropertyInt64Stat, EmoteField.Amount64 } },
            { EmoteType.SetMyQuestBitsOff, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.SetMyQuestBitsOn, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.SetMyQuestCompletions, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.SetSanctuaryPosition, new List<EmoteField>() { EmoteField.Position } },
            { EmoteType.SetQuestBitsOff, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.SetQuestBitsOn, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.SetQuestCompletions, new List<EmoteField>() { EmoteField.Message, EmoteField.Amount } },
            { EmoteType.SpendLuminance, new List<EmoteField>() { EmoteField.HeroXP64 } },
            { EmoteType.Sound, new List<EmoteField>() { EmoteField.Sound } },
            { EmoteType.StampFellowQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.StampMyQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.StampQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.StartEvent, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.StopEvent, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.TakeItems, new List<EmoteField>() { EmoteField.WeenieClassId, EmoteField.StackSize } },
            { EmoteType.TeachSpell, new List<EmoteField>() { EmoteField.SpellId } },
            { EmoteType.TeleportSelf, new List<EmoteField>() { EmoteField.Position } },
            { EmoteType.TeleportTarget, new List<EmoteField>() { EmoteField.Position } },
            { EmoteType.Tell, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.TellFellow, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.TextDirect, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.Turn, new List<EmoteField>() { EmoteField.Angles } },
            { EmoteType.UntrainSkill, new List<EmoteField>() { EmoteField.SkillStat } },
            { EmoteType.UpdateFellowQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.UpdateMyQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.UpdateQuest, new List<EmoteField>() { EmoteField.Message } },
            { EmoteType.WorldBroadcast, new List<EmoteField>() { EmoteField.Message } },
        };

        public static Dictionary<EmoteType, int> RequiredFields = new Dictionary<EmoteType, int>()
        {
            { EmoteType.AwardLevelProportionalSkillXP, 2 },
            { EmoteType.AwardSkillXP, 2 },
            { EmoteType.InqAttributeStat, 2 },
            { EmoteType.InqFloatStat, 2 },
            { EmoteType.InqIntStat, 2 },
            { EmoteType.InqInt64Stat, 2 },
            { EmoteType.InqRawAttributeStat, 2 },
            { EmoteType.InqRawSecondaryAttributeStat, 2 },
            { EmoteType.InqRawSkillStat, 2 },
            { EmoteType.InqSecondaryAttributeStat, 2 },
            { EmoteType.InqSkillStat, 2 },
            { EmoteType.InqStringStat, 2 },
            { EmoteType.SetFloatStat, 2 },
            { EmoteType.SetIntStat, 2 },
            { EmoteType.SetInt64Stat, 2 },
        };
    }
}
