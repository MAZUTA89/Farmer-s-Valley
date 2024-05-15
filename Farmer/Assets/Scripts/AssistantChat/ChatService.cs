using Scripts.SO.Chat;

namespace Scripts.ChatAssistant
{
    public class ChatService
    {
        MassagePanelsFactories _massagePanelsFactories;
        ChatSO _chatSO;
        
        public ChatService(MassagePanelsFactories massagePanelsFactories, 
            ChatSO chatSO)
        {
            _massagePanelsFactories = massagePanelsFactories;
            _chatSO = chatSO;
        }

        public void Initialize()
        {

        }
    }
}
