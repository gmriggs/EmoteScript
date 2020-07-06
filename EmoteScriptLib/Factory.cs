using System;

using EmoteScriptLib.Emotes;
using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib
{
    public static class Factory
    {
        public static Emote Create(EmoteType type)
        {
            switch (type)
            {
                case EmoteType.Act:
                    return new Act();

                case EmoteType.Activate:
                    return new Activate();

                case EmoteType.AddCharacterTitle:
                    return new AddCharacterTitle();

                case EmoteType.AddContract:
                    return new AddContract();

                case EmoteType.AdminSpam:
                    return new AdminSpam();

                case EmoteType.AwardLevelProportionalSkillXP:
                    return new AwardLevelProportionalSkillXP();

                case EmoteType.AwardLevelProportionalXP:
                    return new AwardLevelProportionalXP();

                case EmoteType.AwardLuminance:
                    return new AwardLuminance();

                case EmoteType.AwardNoShareXP:
                    return new AwardNoShareXP();

                case EmoteType.AwardSkillPoints:
                    return new AwardSkillPoints();

                case EmoteType.AwardSkillXP:
                    return new AwardSkillXP();

                case EmoteType.AwardTrainingCredits:
                    return new AwardTrainingCredits();

                case EmoteType.AwardXP:
                    return new AwardXP();

                case EmoteType.BLog:
                    return new BLog();

                case EmoteType.CastSpell:
                    return new CastSpell();

                case EmoteType.CastSpellInstant:
                    return new CastSpellInstant();

                case EmoteType.CloseMe:
                    return new CloseMe();

                case EmoteType.CreateTreasure:
                    return new CreateTreasure();

                case EmoteType.DecrementIntStat:
                    return new DecrementIntStat();

                case EmoteType.DecrementMyQuest:
                    return new DecrementMyQuest();

                case EmoteType.DecrementQuest:
                    return new DecrementQuest();

                case EmoteType.DeleteSelf:
                    return new DeleteSelf();

                case EmoteType.DirectBroadcast:
                    return new DirectBroadcast();

                case EmoteType.EraseMyQuest:
                    return new EraseMyQuest();

                case EmoteType.EraseQuest:
                    return new EraseQuest();

                case EmoteType.FellowBroadcast:
                    return new FellowBroadcast();

                case EmoteType.ForceMotion:
                    return new ForceMotion();

                case EmoteType.Generate:
                    return new Generate();

                case EmoteType.Give:
                    return new Give();

                case EmoteType.Goto:
                    return new Goto();

                case EmoteType.IncrementIntStat:
                    return new IncrementIntStat();

                case EmoteType.IncrementMyQuest:
                    return new IncrementMyQuest();

                case EmoteType.IncrementQuest:
                    return new IncrementQuest();

                case EmoteType.InflictVitaePenalty:
                    return new InflictVitaePenalty();

                case EmoteType.InqAttributeStat:
                    return new InqAttributeStat();

                case EmoteType.InqBoolStat:
                    return new InqBoolStat();

                case EmoteType.InqContractsFull:
                    return new InqContractsFull();

                case EmoteType.InqEvent:
                    return new InqEvent();

                case EmoteType.InqFellowNum:
                    return new InqFellowNum();

                case EmoteType.InqFellowQuest:
                    return new InqFellowQuest();

                case EmoteType.InqFloatStat:
                    return new InqFloatStat();

                case EmoteType.InqInt64Stat:
                    return new InqInt64Stat();

                case EmoteType.InqIntStat:
                    return new InqIntStat();

                case EmoteType.InqMyQuest:
                    return new InqMyQuest();

                case EmoteType.InqMyQuestBitsOff:
                    return new InqMyQuestBitsOff();
                
                case EmoteType.InqMyQuestBitsOn:
                    return new InqMyQuestBitsOn();

                case EmoteType.InqMyQuestSolves:
                    return new InqMyQuestSolves();

                case EmoteType.InqNumCharacterTitles:
                    return new InqNumCharacterTitles();

                case EmoteType.InqOwnsItems:
                    return new InqOwnsItems();

                case EmoteType.InqPackSpace:
                    return new InqPackSpace();

                case EmoteType.InqQuest:
                    return new InqQuest();

                case EmoteType.InqQuestBitsOff:
                    return new InqQuestBitsOff();

                case EmoteType.InqQuestBitsOn:
                    return new InqQuestBitsOn();

                case EmoteType.InqQuestSolves:
                    return new InqQuestSolves();

                case EmoteType.InqRawAttributeStat:
                    return new InqRawAttributeStat();

                case EmoteType.InqRawSecondaryAttributeStat:
                    return new InqRawSecondaryAttributeStat();

                case EmoteType.InqRawSkillStat:
                    return new InqRawSkillStat();

                case EmoteType.InqSecondaryAttributeStat:
                    return new InqSecondaryAttributeStat();

                case EmoteType.InqSkillSpecialized:
                    return new InqSkillSpecialized();

                case EmoteType.InqSkillStat:
                    return new InqSkillStat();

                case EmoteType.InqSkillTrained:
                    return new InqSkillTrained();

                case EmoteType.InqStringStat:
                    return new InqStringStat();

                case EmoteType.InqYesNo:
                    return new InqYesNo();

                case EmoteType.KillSelf:
                    return new KillSelf();

                case EmoteType.LocalBroadcast:
                    return new LocalBroadcast();

                case EmoteType.LocalSignal:
                    return new LocalSignal();

                case EmoteType.LockFellow:
                    return new LockFellow();

                case EmoteType.Motion:
                    return new Motion();

                case EmoteType.Move:
                    return new Move();

                case EmoteType.MoveHome:
                    return new MoveHome();

                case EmoteType.MoveToPos:
                    return new MoveToPos();

                case EmoteType.OpenMe:
                    return new OpenMe();

                case EmoteType.PetCastSpellOnOwner:
                    return new PetCastSpellOnOwner();

                case EmoteType.PhysScript:
                    return new PhysScript();

                case EmoteType.PopUp:
                    return new PopUp();

                case EmoteType.RemoveContract:
                    return new RemoveContract();

                case EmoteType.RemoveVitaePenalty:
                    return new RemoveVitaePenalty();

                case EmoteType.ResetHomePosition:
                    return new ResetHomePosition();

                case EmoteType.Say:
                    return new Say();

                case EmoteType.SetBoolStat:
                    return new SetBoolStat();

                case EmoteType.SetFloatStat:
                    return new SetFloatStat();

                case EmoteType.SetInt64Stat:
                    return new SetInt64Stat();

                case EmoteType.SetIntStat:
                    return new SetIntStat();

                case EmoteType.SetMyQuestCompletions:
                    return new SetMyQuestCompletions();

                case EmoteType.SetQuestCompletions:
                    return new SetQuestCompletions();

                case EmoteType.SetSanctuaryPosition:
                    return new SetSanctuaryPosition();

                case EmoteType.Sound:
                    return new Emotes.Sound();

                case EmoteType.SpendLuminance:
                    return new SpendLuminance();

                case EmoteType.StampFellowQuest:
                    return new StampFellowQuest();

                case EmoteType.StampMyQuest:
                    return new StampMyQuest();

                case EmoteType.StampQuest:
                    return new StampQuest();

                case EmoteType.StartBarber:
                    return new StartBarber();

                case EmoteType.StartEvent:
                    return new StartEvent();

                case EmoteType.StopEvent:
                    return new StopEvent();

                case EmoteType.TakeItems:
                    return new TakeItems();

                case EmoteType.TeachSpell:
                    return new TeachSpell();

                case EmoteType.TeleportTarget:
                    return new TeleportTarget();

                case EmoteType.Tell:
                    return new Tell();

                case EmoteType.TellFellow:
                    return new TellFellow();

                case EmoteType.TextDirect:
                    return new TextDirect();

                case EmoteType.Turn:
                    return new Turn();

                case EmoteType.TurnToTarget:
                    return new TurnToTarget();

                case EmoteType.UntrainSkill:
                    return new UntrainSkill();

                case EmoteType.UpdateFellowQuest:
                    return new UpdateFellowQuest();

                case EmoteType.UpdateMyQuest:
                    return new UpdateMyQuest();

                case EmoteType.UpdateQuest:
                    return new UpdateQuest();

                case EmoteType.WorldBroadcast:
                    return new WorldBroadcast();

                default:

                    Console.WriteLine($"EmoteFactory.Create({type}): unknown emote type");
                    return new Emote();
            }
        }
    }
}
