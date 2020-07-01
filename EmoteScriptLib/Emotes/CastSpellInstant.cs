using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
