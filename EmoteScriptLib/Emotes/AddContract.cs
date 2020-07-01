using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
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
