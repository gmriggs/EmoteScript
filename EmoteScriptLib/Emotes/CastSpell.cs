using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
