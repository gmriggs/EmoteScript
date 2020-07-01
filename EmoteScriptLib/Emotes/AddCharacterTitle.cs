using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
