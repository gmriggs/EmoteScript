using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

using EmoteScript.Entity;
using EmoteScript.Entity.Enum;

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

        [JsonIgnore]
        public List<EmoteCategory> EmoteBranches { get; set; }

        [JsonIgnore]
        public bool HasBranches => EmoteBranches != null;

        [JsonIgnore]
        public Dictionary<EmoteCategory, Branch> Branches { get; set; }

        [JsonIgnore]
        public bool HasBranchesCompleted => Branches.Count == EmoteBranches.Count;

        [JsonIgnore]
        public List<EmoteCategory> RemainingBranches => EmoteBranches.Except(Branches.Keys).ToList();

        public Emote() { }

        public Emote(EmoteType type)
        {
            Type = type;

            EmoteBranches = GetEmoteBranches(type);

            if (EmoteBranches != null)
                Branches = new Dictionary<EmoteCategory, Branch>();
        }

        public Emote(Emote emote)
        {
            // shallow copy constructor
            Type = emote.Type;
            Delay = emote.Delay;
            Extent = emote.Extent;
            Motion = emote.Motion;
            Message = emote.Message;
            TestString = emote.TestString;
            Min = emote.Min;
            Max = emote.Max;
            Min64 = emote.Min64;
            Max64 = emote.Max64;
            MinFloat = emote.MinFloat;
            MaxFloat = emote.MaxFloat;
            Stat = emote.Stat;
            Display = emote.Display;
            Amount = emote.Amount;
            Amount64 = emote.Amount64;
            HeroXP64 = emote.HeroXP64;
            Percent = emote.Percent;
            SpellId = emote.SpellId;
            WealthRating = emote.WealthRating;
            TreasureClass = emote.TreasureClass;
            TreasureType = emote.TreasureType;
            PScript = emote.PScript;
            Sound = emote.Sound;
            DestinationType = emote.DestinationType;
            WeenieClassId = emote.WeenieClassId;
            StackSize = emote.StackSize;
            Palette = emote.Palette;
            Shade = emote.Shade;
            TryToBond = emote.TryToBond;
            ObjCellId = emote.ObjCellId;
            OriginX = emote.OriginX;
            OriginY = emote.OriginY;
            OriginZ = emote.OriginZ;
            AnglesW = emote.AnglesW;
            AnglesX = emote.AnglesX;
            AnglesY = emote.AnglesY;
            AnglesZ = emote.AnglesZ;
        }

        public void AddBranch(Branch branch)
        {
            Branches.Add(branch.Category, branch);
        }
        
        public void AddBranches(List<EmoteCategory> branches)
        {
            EmoteBranches = branches;

            Branches = new Dictionary<EmoteCategory, Branch>();
        }

        public static List<EmoteCategory> GetEmoteBranches(EmoteType type)
        {
            List<EmoteCategory> branches = null;
            
            switch (type)
            {
                case EmoteType.Goto:
                    
                    branches = Branch.GotoSet;
                    break;

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

                    branches = Branch.Test;
                    break;

                case EmoteType.InqQuest:
                case EmoteType.InqQuestBitsOff:
                case EmoteType.InqQuestBitsOn:
                case EmoteType.InqMyQuestSolves:
                case EmoteType.InqQuestSolves:
                case EmoteType.UpdateMyQuest:
                case EmoteType.UpdateQuest:

                    branches = Branch.Quest;
                    break;

                case EmoteType.InqEvent:

                    branches = Branch.Event;
                    break;

                case EmoteType.InqFellowNum:

                    branches = Branch.TestFellow;
                    break;

                case EmoteType.InqFellowQuest:
                case EmoteType.UpdateFellowQuest:

                    branches = Branch.QuestFellow;
                    break;
            }

            return branches;
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

        public string GetMessageKey()
        {
            if (!HasBranches)
                return Message;

            string key = Message;

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

                var rangeNum = minRange ?? maxRange;

                var div = Message != null ? "@" : "-";
                
                key += $"{div}{rangeNum}";
            }

            return key;
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

        public List<string> BuildScript(int depth = 1)
        {
            var lines = new List<string>();

            var indent = string.Concat(Enumerable.Repeat("    ", depth));

            lines.Add($"{indent}- {Type}");

            if (Branches == null)
                return lines;

            foreach (var branch in Branches.Values)
            {
                lines.Add($"{indent}    {branch.Category}:");

                foreach (var emote in branch.Emotes)
                    lines.AddRange(emote.BuildScript(depth + 2));
            }

            return lines;
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

        public virtual bool Parse()
        {
            Console.WriteLine($"Undefined Emote.Parse() called for EmoteType.{Type}");
            return false;
        }

        public override string ToString()
        {
            var fields = new List<string>();

            fields.Add($"Type: {Type}");

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
                fields.Add($"Delay: {Delay}");
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

            return string.Join(", ", fields);
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
    }
}
