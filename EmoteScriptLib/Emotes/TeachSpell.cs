using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class TeachSpell : Emote
    {
        public TeachSpell() : base(EmoteType.TeachSpell)
        {

        }
        
        public TeachSpell(SpellId spell)

            : base(EmoteType.TeachSpell)
        {
            SpellId = spell;
        }
    }
}
