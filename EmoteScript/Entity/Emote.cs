using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using EmoteScript.Entity;
using EmoteScript.Entity.Enum;
using EmoteScript.StringMap;

using Newtonsoft.Json;

namespace EmoteScript
{
    public class Emote
    {
        [JsonIgnore]
        public EmoteSet EmoteSet { get; set; }
        public EmoteType Type { get; set; }
        public float? Delay { get; set; }
        public float? Extent { get; set; }
        public MotionCommand? Motion { get; set; }
        public string Message { get; set; }
        public string TestString { get; set; }
        public int? Min { get; set; }
        public int? Max { get; set; }
        public long? Min64 { get; set; }
        public long? Max64 { get; set; }
        public float? MinFloat { get; set; }
        public float? MaxFloat { get; set; }
        public int? Stat { get; set; }
        [JsonIgnore]
        public bool? Display { get; set; }
        public int? Amount { get; set; }
        public long? Amount64 { get; set; }
        public long? HeroXP64 { get; set; }
        public double? Percent { get; set; }
        public SpellId? SpellId { get; set; }
        public int? WealthRating { get; set; }
        public int? TreasureClass { get; set; }
        public int? TreasureType { get; set; }
        public PlayScript? PScript { get; set; }
        public Sound? Sound { get; set; }
        public DestinationType? DestinationType { get; set; }
        public uint? WeenieClassId { get; set; }
        public int? StackSize { get; set; }
        public int? Palette { get; set; }
        public float? Shade { get; set; }
        public bool? TryToBond { get; set; }
        public uint? ObjCellId { get; set; }
        public float? OriginX { get; set; }
        public float? OriginY { get; set; }
        public float? OriginZ { get; set; }
        public float? AnglesW { get; set; }
        public float? AnglesX { get; set; }
        public float? AnglesY { get; set; }
        public float? AnglesZ { get; set; }

        /// <summary>
        /// Returns the list of EmoteSet categories this Emote type
        /// can possibly branch to
        /// </summary>
        [JsonIgnore]
        public List<EmoteCategory> ValidBranches;

        /// <summary>
        /// Returns TRUE if this emote branches to other EmoteSet categories,
        /// an Inq* emote, for example
        /// </summary>
        [JsonIgnore]
        public bool HasBranches => ValidBranches != null;

        /// <summary>
        /// A list of links to the EmoteSet categories this Emote
        /// can possibly branch to
        /// </summary>
        [JsonIgnore]
        public List<EmoteSet> Branches { get; set; }

        [JsonIgnore]
        public bool HasBranchesCompleted
        {
            get
            {
                var completed = new HashSet<EmoteCategory>();

                foreach (var branch in Branches)
                {
                    // incremental format
                    var totalProbability = branch.Probability ?? 1.0f;

                    if (totalProbability == 1.0f)
                        completed.Add(branch.Category);
                }

                return completed.Count == ValidBranches.Count;
            }
        }

        [JsonIgnore]
        public List<EmoteCategory> RemainingBranches => ValidBranches.Except(Branches.Select(i => i.Category)).ToList();

        public Emote() { }

        public Emote(EmoteType type)
        {
            Type = type;

            ValidBranches = GetValidBranches(type);

            if (ValidBranches != null)
                Branches = new List<EmoteSet>();
        }

        public void AddBranch(EmoteSet emoteSet)
        {
            Branches.Add(emoteSet);
        }
        
        public void AddValidBranches(List<EmoteCategory> branches)
        {
            ValidBranches = branches;

            Branches = new List<EmoteSet>();
        }

        /// <summary>
        /// Returns the list of EmoteSet categories an Emote type
        /// can possibly branch to
        /// </summary>
        public static List<EmoteCategory> GetValidBranches(EmoteType type)
        {
            switch (type)
            {
                case EmoteType.Goto:

                    return Branch.GotoSet;

                case EmoteType.InqAttributeStat:
                case EmoteType.InqBoolStat:
                case EmoteType.InqContractsFull:
                case EmoteType.InqFloatStat:
                case EmoteType.InqIntStat:
                case EmoteType.InqInt64Stat:
                case EmoteType.InqMyQuest:
                case EmoteType.InqMyQuestBitsOff:
                case EmoteType.InqMyQuestBitsOn:
                case EmoteType.InqNumCharacterTitles:
                case EmoteType.InqOwnsItems:
                case EmoteType.InqPackSpace:
                case EmoteType.InqRawAttributeStat:
                case EmoteType.InqRawSecondaryAttributeStat:
                case EmoteType.InqRawSkillStat:
                case EmoteType.InqSecondaryAttributeStat:
                case EmoteType.InqSkillSpecialized:
                case EmoteType.InqSkillStat:
                case EmoteType.InqSkillTrained:
                case EmoteType.InqStringStat:
                case EmoteType.InqYesNo:

                    return Branch.Test;

                case EmoteType.InqQuest:
                case EmoteType.InqQuestBitsOff:
                case EmoteType.InqQuestBitsOn:
                case EmoteType.InqMyQuestSolves:
                case EmoteType.InqQuestSolves:
                case EmoteType.UpdateMyQuest:
                case EmoteType.UpdateQuest:

                    return Branch.Quest;

                case EmoteType.InqEvent:

                    return Branch.Event;

                case EmoteType.InqFellowNum:

                    return Branch.TestFellow;

                case EmoteType.InqFellowQuest:
                case EmoteType.UpdateFellowQuest:

                    return Branch.QuestFellow;

                default:

                    return null;
            }
        }

        public void SetOrigin(Vector3? origin)
        {
            OriginX = origin?.X;
            OriginY = origin?.Y;
            OriginZ = origin?.Z;
        }

        public void SetOrientation(Quaternion? orientation)
        {
            AnglesW = orientation?.W;
            AnglesX = orientation?.X;
            AnglesY = orientation?.Y;
            AnglesZ = orientation?.Z;
        }

        public void SetPosition(Position pos)
        {
            ObjCellId = pos?.ObjCellId;

            SetOrigin(pos?.Frame?.Origin);

            SetOrientation(pos?.Frame?.Orientation);
        }

        public static HashSet<string> GlobalKeys = new HashSet<string>();
        
        public string GetMessageKey()
        {
            if (!HasBranches)
                return Message;

            string key = Message;

            if (key != null)
            {
                if (Type.ToString().Contains("Quest"))
                {
                    if (key.Contains("@"))
                        return key;
                }
                else
                    return key;
            }

            switch (Type)
            {
                case EmoteType.InqAttributeStat:
                case EmoteType.InqRawAttributeStat:
                    key = ((PropertyAttribute)Stat).ToString();
                    break;

                case EmoteType.InqBoolStat:
                    key = ((PropertyBool)Stat).ToString();
                    break;

                case EmoteType.InqContractsFull:
                    key = "ContractsFull";
                    break;

                case EmoteType.InqFloatStat:
                    key = ((PropertyFloat)Stat).ToString();
                    break;

                case EmoteType.InqIntStat:
                    key = ((PropertyInt)Stat).ToString();
                    break;

                case EmoteType.InqInt64Stat:
                    key = ((PropertyInt64)Stat).ToString();
                    break;

                case EmoteType.InqNumCharacterTitles:
                    key = "NumCharacterTitles";
                    break;

                case EmoteType.InqOwnsItems:
                    key = $"OwnsItem-{WeenieClassId}";
                    break;

                case EmoteType.InqPackSpace:
                    key = "PackSpace";
                    break;

                case EmoteType.InqRawSecondaryAttributeStat:
                case EmoteType.InqSecondaryAttributeStat:
                    key = ((PropertyAttribute2nd)Stat).ToString();
                    break;

                case EmoteType.InqSkillStat:
                case EmoteType.InqRawSkillStat:
                    key = ((Skill)Stat).ToString();
                    break;

                case EmoteType.InqSkillSpecialized:
                    key = ((Skill)Stat).ToString() + "Spec";
                    break;

                case EmoteType.InqSkillTrained:
                    key = ((Skill)Stat).ToString() + "Trained";
                    break;

                case EmoteType.InqStringStat:
                    key = ((PropertyString)Stat).ToString();
                    break;

                case EmoteType.InqYesNo:
                    key = "InqYesNo";
                    break;

                case EmoteType.InqFellowNum:
                    key = "HasFellowship";
                    break;
            }

            if (IsRangeType)
            {
                var minRange = Min ?? Min64 ?? MinFloat;
                var maxRange = Max ?? Max64 ?? MaxFloat;

                var rangeStr = $"{minRange}";
                if (maxRange != null)
                {
                    if (rangeStr != null)
                        rangeStr += $"-{maxRange}";
                    else
                        rangeStr = $"{maxRange}";
                }

                var div = Message != null ? "@" : "_";
                
                if (!string.IsNullOrEmpty(rangeStr))
                    key += $"{div}{rangeStr}";
            }

            var newKey = key;
            var i = 2;
            while (GlobalKeys.Contains(newKey))
            {
                newKey = key + $"_{i++}";
            }
            GlobalKeys.Add(newKey);

            return newKey;
        }

        [JsonIgnore]
        public bool IsRangeType
        {
            get
            {
                switch (Type)
                {
                    case EmoteType.InqAttributeStat:
                    case EmoteType.InqFloatStat:
                    case EmoteType.InqIntStat:
                    case EmoteType.InqInt64Stat:
                    case EmoteType.InqNumCharacterTitles:
                    case EmoteType.InqOwnsItems:
                    case EmoteType.InqPackSpace:
                    case EmoteType.InqRawAttributeStat:
                    case EmoteType.InqRawSecondaryAttributeStat:
                    case EmoteType.InqRawSkillStat:
                    case EmoteType.InqSecondaryAttributeStat:
                    case EmoteType.InqSkillStat:
                    case EmoteType.InqMyQuestSolves:
                    case EmoteType.InqQuestSolves:
                        return true;
                }
                return false;
            }
        }

        public EmoteField GetPopulatedFields()
        {
            var fields = EmoteField.None;

            if (Delay != null)
                fields |= EmoteField.Delay;
            if (Extent != null)
                fields |= EmoteField.Extent;
            if (Motion != null)
                fields |= EmoteField.Motion;
            if (Message != null)
                fields |= EmoteField.Message;
            if (TestString != null)
                fields |= EmoteField.TestString;
            if (Min != null)
                fields |= EmoteField.Min;
            if (Max != null)
                fields |= EmoteField.Max;
            if (Min64 != null)
                fields |= EmoteField.Min64;
            if (Max64 != null)
                fields |= EmoteField.Max64;
            if (MinFloat != null)
                fields |= EmoteField.MinFloat;
            if (MaxFloat != null)
                fields |= EmoteField.MaxFloat;
            if (Stat != null)
                fields |= EmoteField.Stat;
            if (Display != null)
                fields |= EmoteField.Display;
            if (Amount != null)
                fields |= EmoteField.Amount;
            if (Amount64 != null)
                fields |= EmoteField.Amount64;
            if (Percent != null)
                fields |= EmoteField.Percent;
            if (SpellId != null)
                fields |= EmoteField.SpellId;
            if (WealthRating != null)
                fields |= EmoteField.WealthRating;
            if (TreasureClass != null)
                fields |= EmoteField.TreasureClass;
            if (TreasureType != null)
                fields |= EmoteField.TreasureType;
            if (PScript != null)
                fields |= EmoteField.PScript;
            if (Sound != null)
                fields |= EmoteField.Sound;
            if (DestinationType != null)
                fields |= EmoteField.DestinationType;
            if (WeenieClassId != null)
                fields |= EmoteField.WeenieClassId;
            if (StackSize != null)
                fields |= EmoteField.StackSize;
            if (Palette != null)
                fields |= EmoteField.Palette;
            if (Shade != null)
                fields |= EmoteField.Shade;
            if (TryToBond != null)
                fields |= EmoteField.TryToBond;
            if (ObjCellId != null)
                fields |= EmoteField.ObjCellId;
            if (OriginX != null)
                fields |= EmoteField.OriginX;
            if (OriginY != null)
                fields |= EmoteField.OriginY;
            if (OriginZ != null)
                fields |= EmoteField.OriginZ;
            if (AnglesW != null)
                fields |= EmoteField.AnglesW;
            if (AnglesX != null)
                fields |= EmoteField.AnglesX;
            if (AnglesY != null)
                fields |= EmoteField.AnglesY;
            if (AnglesZ != null)
                fields |= EmoteField.AnglesZ;

            return fields;
        }

        public override string ToString()
        {
            var result = $"{Type}";

            var fields = new List<string>();

            if (Delay != null)
                fields.Add($"Delay: {Delay}");
            if (Extent != null)
                fields.Add($"Extent: {Extent}");
            if (Motion != null)
                fields.Add($"Motion: {Motion}");
            if (Stat != null)
                fields.Add($"Stat: {Stat}");
            if (Percent != null)
                fields.Add($"Percent: {Percent}");
            if (Min != null)
                fields.Add($"Min: {Min}");
            if (Max != null)
                fields.Add($"Max: {Max}");
            if (Min64 != null)
                fields.Add($"Min64: {Min64}");
            if (Max64 != null)
                fields.Add($"Max64: {Max64}");
            if (MinFloat != null)
                fields.Add($"MinFloat: {MinFloat}");
            if (MaxFloat != null)
                fields.Add($"MaxFloat: {MaxFloat}");
            if (TestString != null)
                fields.Add($"TestString: {TestString}");
            if (Message != null)
                fields.Add($"Message: {Message}");
            if (Display != null)
                fields.Add($"Display: {Display}");
            if (Amount != null)
                fields.Add($"Amount: {Amount}");
            if (Amount64 != null)
                fields.Add($"Amount64: {Amount64}");
            if (HeroXP64 != null)
                fields.Add($"HeroXP64: {HeroXP64}");
            if (SpellId != null)
                fields.Add($"SpellId: {SpellId}");
            if (WealthRating != null)
                fields.Add($"WealthRating: {WealthRating}");
            if (TreasureClass != null)
                fields.Add($"TreasureClass: {TreasureClass}");
            if (TreasureType != null)
                fields.Add($"TreasureType: {TreasureType}");
            if (PScript != null)
                fields.Add($"PScript: {PScript}");
            if (Sound != null)
                fields.Add($"Sound: {Sound}");
            if (DestinationType != null)
                fields.Add($"DestinationType: {DestinationType}");
            if (WeenieClassId != null)
                fields.Add($"WeenieClassId: {WeenieClassId}");
            if (StackSize != null)
                fields.Add($"StackSize: {StackSize}");
            if (Palette != null)
                fields.Add($"Palette: {Palette}");
            if (Shade != null)
                fields.Add($"Shade: {Shade}");
            if (TryToBond != null)
                fields.Add($"TryToBond: {TryToBond}");
            if (ObjCellId != null)
                fields.Add($"ObjCellId: 0x{ObjCellId:X8}");
            if (OriginX != null)
                fields.Add($"OriginX: {OriginX}");
            if (OriginY != null)
                fields.Add($"OriginY: {OriginY}");
            if (OriginZ != null)
                fields.Add($"OriginZ: {OriginZ}");
            if (AnglesW != null)
                fields.Add($"AnglesW: {AnglesW}");
            if (AnglesX != null)
                fields.Add($"AnglesX: {AnglesX}");
            if (AnglesY != null)
                fields.Add($"AnglesY: {AnglesY}");
            if (AnglesZ != null)
                fields.Add($"AnglesZ: {AnglesZ}");

            if (fields.Count > 0)
                result += $": {string.Join(", ", fields)}";

            return result;
        }

        public string ToString(bool fluent)
        {
            if (!fluent)
                return ToString();

            var result = "";

            if (Delay != null && Delay != 0.0f)
                result += $"Delay: {Delay}, ";

            result += Type;

            var fluentString = GetFluentString();

            if (fluentString?.Length > 0)
                result += ": " + fluentString;

            return result;
        }

        public string GetFluentString()
        {
            var message = (Branches?.FirstOrDefault()?.Inline ?? false) ? "" : $", {Message}";

            switch (Type)
            {
                case EmoteType.AwardNoShareXP:
                case EmoteType.AwardXP:
                    return $"{Amount64:N0}";

                case EmoteType.AddCharacterTitle:
                    return $"{(CharacterTitle)Amount}";

                case EmoteType.AddContract:
                    return $"{(ContractId)Stat}";

                case EmoteType.AwardLevelProportionalXP:
                    var suffix = "";
                    if ((Max64 ?? 0) != 0)
                        suffix += $", {Max64:N0}";
                    if ((Min64 ?? 0) != 0 && Min64 != Max64)
                        suffix = $", {Min64:N0} - {Max64:N0}";
                    if (Display ?? false)
                        suffix += $", Share";
                    return $"{Math.Round((Percent ?? 0) * 100, 2)}%{suffix}";

                case EmoteType.AwardLevelProportionalSkillXP:
                    suffix = "";
                    if ((Max ?? 0) != 0)
                        suffix += $", {Max:N0}";
                    if ((Min ?? 0) != 0 && Min != Max)
                        suffix = $", {Min:N0} - {Max:N0}";
                    return $"{((Skill)Stat).ToSentence()}, {Math.Round((Percent ?? 0) * 100, 2)}%{suffix}";

                case EmoteType.AwardLuminance:
                case EmoteType.SpendLuminance:
                    return $"{HeroXP64:N0}";

                case EmoteType.AwardSkillXP:
                case EmoteType.AwardSkillPoints:
                    return $"{((Skill)Stat).ToSentence()}, {Amount:N0}";

                case EmoteType.CreateTreasure:
                    return $"Type: {TreasureType}, Class: {TreasureClass}, WealthRating: {WealthRating}";

                case EmoteType.SetFloatStat:
                    return $"{(PropertyFloat)Stat}, {Percent}";

                case EmoteType.DecrementIntStat:
                case EmoteType.IncrementIntStat:
                    suffix = (Amount ?? 1) > 1 ? $", {Amount:N0}" : "";
                    return $"{(PropertyInt)Stat}{suffix}";

                case EmoteType.SetIntStat:
                    return $"{(PropertyInt)Stat}, {Amount:N0}";

                case EmoteType.SetInt64Stat:
                    return $"{(PropertyInt64)Stat}, {Amount64:N0}";

                case EmoteType.SetBoolStat:
                    return $"{(PropertyBool)Stat}, {(Amount == 0 ? "False" : "True")}";

                case EmoteType.IncrementMyQuest:
                case EmoteType.IncrementQuest:
                case EmoteType.AwardTrainingCredits:
                case EmoteType.InflictVitaePenalty:
                    var amount = (Amount ?? 1) > 1 ? $", {Amount}" : "";
                    return $"{Message}{amount}";

                case EmoteType.InqAttributeStat:
                case EmoteType.InqRawAttributeStat:

                    amount = $"{Min:N0}";
                    if (Max != Min)
                        amount += $" - {Max:N0}";

                    return $"{(PropertyAttribute)Stat}, {amount}{message}";

                case EmoteType.InqBoolStat:

                    return $"{(PropertyBool)Stat}{message}";

                case EmoteType.InqFloatStat:

                    amount = $"{MinFloat}";
                    if (MaxFloat != MinFloat)
                        amount += $" - {MaxFloat}";

                    return $"{(PropertyFloat)Stat}, {amount}{message}";

                case EmoteType.InqIntStat:

                    amount = $"{Min:N0}";
                    if (Max != Min)
                        amount += $" - {Max:N0}";

                    return $"{(PropertyInt)Stat}, {amount}{message}";

                case EmoteType.InqInt64Stat:

                    amount = $"{Min64:N0}";
                    if (Max64 != Min64)
                        amount += $" - {Max64:N0}{message}";

                    return $"{(PropertyInt64)Stat}, {amount}";

                case EmoteType.InqQuestSolves:
                    var numSolves = Min != null ? $", {Min}" : "";
                    if (Max != null && Min != Max)
                        numSolves += $" - {Max}";
                    return $"{Message}{numSolves}";

                case EmoteType.InqSecondaryAttributeStat:
                case EmoteType.InqRawSecondaryAttributeStat:

                    amount = $"{Min:N0}";
                    if (Max != Min)
                        amount += $" - {Max:N0}{message}";

                    return $"{(PropertyAttribute2nd)Stat}, {amount}";

                case EmoteType.InqSkillStat:
                case EmoteType.InqRawSkillStat:

                    amount = $"{Min:N0}";
                    if (Max != Min)
                        amount += $" - {Max:N0}{message}";

                    return $"{((Skill)Stat).ToSentence()}, {amount}";

                case EmoteType.InqStringStat:
                    return $"{(PropertyString)Stat}, \"{TestString}\"{message}";

                case EmoteType.InqSkillTrained:
                case EmoteType.InqSkillSpecialized:
                    return $"{((Skill)Stat).ToSentence()}{message}";

                case EmoteType.UntrainSkill:
                    return $"{((Skill)Stat).ToSentence()}";

                case EmoteType.InqYesNo:
                    return TestString + message;

                case EmoteType.Give:
                case EmoteType.TakeItems:

                    var wcid = WeenieClassId.Value;
                    var stackSize = (StackSize ?? 1) > 1 ? $", {StackSize:N0}" : "";
                    return $"{WeenieName}{stackSize}";

                case EmoteType.InqOwnsItems:

                    wcid = WeenieClassId.Value;
                    stackSize = (StackSize ?? 1) > 1 ? $", {StackSize:N0}" : "";
                    return $"{WeenieName}{stackSize}{message}";

                case EmoteType.Motion:
                case EmoteType.ForceMotion:
                    return $"{(MotionCommand)Motion}";

                case EmoteType.Move:
                case EmoteType.MoveToPos:
                case EmoteType.SetSanctuaryPosition:
                case EmoteType.TeleportTarget:
                    var moveTo = new Position(ObjCellId ?? 0, new Vector3(OriginX ?? 0, OriginY ?? 0, OriginZ ?? 0),
                        new Quaternion(AnglesX ?? 0, AnglesY ?? 0, AnglesZ ?? 0, AnglesW ?? 1));
                    return moveTo.ToString();

                case EmoteType.Sound:
                    return $"{(Sound)Sound}";

                case EmoteType.CastSpell:
                case EmoteType.CastSpellInstant:
                case EmoteType.TeachSpell:
                case EmoteType.PetCastSpellOnOwner:
                    return SpellName;

                case EmoteType.SetMyQuestCompletions:
                case EmoteType.SetQuestCompletions:
                    return $"{Message}, {Amount}";

                case EmoteType.Turn:
                    var rotation = new Quaternion(AnglesX ?? 0, AnglesY ?? 0, AnglesZ ?? 0, AnglesW ?? 1);
                    var heading = Position.get_heading(rotation);
                    var heading_dir = Position.get_heading_dir(heading);
                    return $"Heading: {heading} ({heading_dir})";
            }

            return Message;
        }

        public static Dictionary<uint, string> WeenieNames;
        public static Dictionary<uint, string> WeenieClassNames;

        [JsonIgnore]
        public string WeenieName
        {
            get
            {
                if (WeenieClassId == null)
                    return null;

                if (WeenieNames == null)
                    WeenieNames = Reader.GetIDToNames("WeenieName.txt");

                if (WeenieClassNames == null)
                    WeenieClassNames = Reader.GetIDToNames("WeenieClassName.txt");

                if (WeenieNames.TryGetValue(WeenieClassId.Value, out var weenieName))
                    return $"{weenieName} ({WeenieClassId})";

                if (WeenieClassNames.TryGetValue(WeenieClassId.Value, out var weenieClassName))
                    return $"{weenieClassName} ({WeenieClassId})";

                return WeenieClassId.ToString();
            }
        }

        public static Dictionary<uint, string> SpellNames;

        [JsonIgnore]
        public string SpellName
        {
            get
            {
                if (SpellId == null)
                    return null;

                if (SpellNames == null)
                    SpellNames = Reader.GetIDToNames("SpellName.txt");

                if (SpellNames.TryGetValue((uint)SpellId, out var spellName))
                    return $"{SpellId} - {spellName}";

                return SpellId.ToString();
            }
        }

        // json
        public int? display => Display != null ? (int?)Convert.ToInt32(Display) : null;

        public Position Position
        {
            get
            {
                if (ObjCellId == null || OriginX == null || OriginY == null || OriginZ == null ||
                    AnglesW == null || AnglesX == null || AnglesY == null || AnglesZ == null)
                {
                    return null;
                }

                return new Position(ObjCellId.Value, new Vector3(OriginX.Value, OriginY.Value, OriginZ.Value),
                    new Quaternion(AnglesX.Value, AnglesY.Value, AnglesZ.Value, AnglesW.Value));
            }
        }

        public Frame Frame
        {
            get
            {
                if (ObjCellId != null || OriginX == null && OriginY == null && OriginZ == null &&
                    AnglesW == null && AnglesX == null && AnglesY == null && AnglesZ == null)
                {
                    return null;
                }

                var origin = Vector3.Zero;
                var orientation = Quaternion.Identity;

                if (OriginX != null && OriginY != null && OriginZ != null)
                    origin = new Vector3(OriginX.Value, OriginY.Value, OriginZ.Value);

                if (AnglesW != null && AnglesX != null && AnglesY != null && AnglesZ != null)
                    orientation = new Quaternion(AnglesX.Value, AnglesY.Value, AnglesZ.Value, AnglesW.Value);

                return new Frame(origin, orientation);
            }
        }

        public void NormalRange()
        {
            if (Min != null && Max == null)
                Max = int.MaxValue;

            if (Max != null && Min == null)
                Min = int.MinValue;

            if (MinFloat != null && MaxFloat == null)
                MaxFloat = float.MaxValue;

            if (MaxFloat != null && MinFloat == null)
                MinFloat = float.MinValue;

            if (Min64 != null && Max64 == null)
                Max64 = long.MaxValue;

            if (Max64 != null && Min64 == null)
                Min64 = long.MinValue;
        }
    }
}
