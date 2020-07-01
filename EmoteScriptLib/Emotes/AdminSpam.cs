using EmoteScript.Entity.Enum;

namespace EmoteScript.Emotes
{
    public class AdminSpam : Emote
    {
        public AdminSpam() : base(EmoteType.AdminSpam)
        {

        }
        
        public AdminSpam(string message) : base(EmoteType.AdminSpam)
        {
            Message = message;
        }
    }
}
