using EmoteScript.Entity.Enum;


namespace EmoteScript.Emotes
{
    public class PopUp : Emote
    {
        public PopUp()
            
            : base(EmoteType.PopUp)
        {

        }
        
        public PopUp(string message)

            : base(EmoteType.PopUp)
        {
            Message = message;
        }
    }
}
