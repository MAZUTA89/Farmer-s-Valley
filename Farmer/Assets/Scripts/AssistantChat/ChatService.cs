using Scripts.SO.Chat;
using OpenAI_API;
using OpenAI_API.Chat;
using System.Threading.Tasks;
using System.Text;

namespace Scripts.ChatAssistant
{
    
    public class ChatService
    {
        ChatSO _chatSO;
        OpenAIAPI _api;
        APIAuthentication _authentication;

        Conversation _chat;

        StringBuilder _massagesTextBuilder;
        
        public ChatService(
            ChatSO chatSO)
        {
            _chatSO = chatSO;
            _authentication = new APIAuthentication(_chatSO.APIKey);

            _api = new OpenAIAPI(_authentication);
            _api.ApiUrlFormat = _chatSO.Url;
            _chat = _api.Chat.CreateConversation();
            _chat.Model.ModelID = _chatSO.ModelId;
            _chat.RequestParameters.Temperature = _chatSO.Temperature;
            _chat.RequestParameters.MaxTokens = _chatSO.MaxTokens;

            _massagesTextBuilder = new StringBuilder();

            AddSystemInput(_chatSO.SystemMassage);
        }

        public void AddUserInput(string text)
        {
            _chat.AppendUserInput(text);
        }

        void AddSystemInput(string text)
        {
            _chat.AppendSystemMessage(text);
        }

        public async Task<string> GetResponseAsync()
        {
            return await _chat.GetResponseFromChatbotAsync();
        }
        public string GetContextText()
        {
            _massagesTextBuilder.Clear();

            foreach (var massage in _chat.Messages)
            {
                _massagesTextBuilder.Append(massage.TextContent);
            }

            return _massagesTextBuilder.ToString();
        }
        public void ClearChat()
        {
            var massages = _chat.Messages;
            for (int i = 0; i < massages.Count; i++)
            {
                if (massages[i].Role.ToString() == "user" ||
                    massages[i].Role.ToString() == "assistant")
                {
                    massages.Remove(massages[i]);
                }
            }
        }
    }
}
