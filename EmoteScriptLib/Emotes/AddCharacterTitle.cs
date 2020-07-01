using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class AddCharacterTitle : Emote
    {
        public AddCharacterTitle() : base(EmoteType.AddCharacterTitle)
        {

        }
        
        public AddCharacterTitle(CharacterTitle title) : base(EmoteType.AddCharacterTitle)
        {
            Stat = (int)title;
        }
    }
}
