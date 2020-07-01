using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
