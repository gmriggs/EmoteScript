using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class CastSpell : Emote
    {
        public CastSpell()
            
            : base(EmoteType.CastSpell)
        {

        }
        
        public CastSpell(SpellId spell)

            : base(EmoteType.CastSpell)
        {
            SpellId = spell;
        }
    }
}
