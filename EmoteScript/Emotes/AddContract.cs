using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class AddContract : Emote
    {
        public AddContract() : base(EmoteType.AddContract)
        {

        }
        
        public AddContract(ContractId contract) : base(EmoteType.AddContract)
        {
            Stat = (int)contract;
        }
    }
}
