using EmoteScriptLib.Entity.Enum;

namespace EmoteScriptLib.Emotes
{
    public class Say : Emote
    {
        public Say() : base(EmoteType.Say)
        {

        }
        
        public Say(string message) : base(EmoteType.Say)
        {
            Message = message;
        }
    }
}
