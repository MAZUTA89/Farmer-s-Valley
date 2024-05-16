using Scripts.SO.Chat;
using OpenAI_API;

namespace Scripts.ChatAssistant
{
    public class ChatService
    {
        MassagePanelsFactories _massagePanelsFactories;
        ChatSO _chatSO;
        
        public ChatService(MassagePanelsFactories massagePanelsFactories, 
            ChatSO chatSO)
        {
           OpenAIAPI openAIAPI = new OpenAIAPI();
            _massagePanelsFactories = massagePanelsFactories;
            _chatSO = chatSO;
        }

        public void Initialize()
        {

        }
    }
}
