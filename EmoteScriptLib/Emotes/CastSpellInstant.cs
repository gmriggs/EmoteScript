using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class CastSpellInstant : Emote
    {
        public CastSpellInstant() : base(EmoteType.CastSpellInstant)
        {

        }
        
        public CastSpellInstant(SpellId spell)

            : base(EmoteType.CastSpellInstant)
        {
            SpellId = spell;
        }
    }
}
