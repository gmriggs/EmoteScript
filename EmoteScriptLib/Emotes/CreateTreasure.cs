using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class CreateTreasure : Emote
    {
        public CreateTreasure()
            
            : base(EmoteType.CreateTreasure)
        {

        }
        
        public CreateTreasure(int? tier = null)

            : base(EmoteType.CreateTreasure)
        {
            WealthRating = tier;
        }
    }
}
