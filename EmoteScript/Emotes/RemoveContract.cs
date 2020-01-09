using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class RemoveContract : Emote
    {
        public RemoveContract() : base(EmoteType.RemoveContract)
        {

        }
        
        public RemoveContract(ContractId contract)

            : base(EmoteType.RemoveContract)
        {
            Stat = (int)contract;
        }
    }
}
