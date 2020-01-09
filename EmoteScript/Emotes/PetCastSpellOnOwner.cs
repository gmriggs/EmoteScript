using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
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
