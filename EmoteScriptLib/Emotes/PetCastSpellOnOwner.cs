using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class PetCastSpellOnOwner : Emote
    {
        public PetCastSpellOnOwner()
            
            : base(EmoteType.PetCastSpellOnOwner)
        {

        }
        
        public PetCastSpellOnOwner(SpellId spell)

            : base(EmoteType.PetCastSpellOnOwner)
        {
            SpellId = spell;
        }
    }
}
