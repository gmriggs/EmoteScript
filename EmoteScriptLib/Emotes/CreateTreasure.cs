using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class CreateTreasure : Emote
    {
        public CreateTreasure()
            
            : base(EmoteType.CreateTreasure)
        {

        }
        
        public CreateTreasure(int? treasureType = null, int? treasureClass = null, int? wealthRating = null)

            : base(EmoteType.CreateTreasure)
        {
            TreasureType = treasureType;        // TreasureItemCategory (Item / MagicItem / MundaneItem)
            TreasureClass = treasureClass;      // TreasureItemType_Orig
            WealthRating = wealthRating;        // tier
        }
    }
}
