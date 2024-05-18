using Scripts.SO.Chat;
using OpenAI_API;
using UnityEngine;
using Zenject;
using OpenAI_API.Chat;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Scripts.ChatAssistant
{
    
    public class ChatService
    {
        MassagePanelsFactories _massagePanelsFactories;
        ChatSO _chatSO;
        OpenAIAPI _api;
        APIAuthentication _authentication;

        Conversation _chat;
        
        public ChatService(MassagePanelsFactories massagePanelsFactories,
            ChatSO chatSO)
        {
            _massagePanelsFactories = massagePanelsFactories;
            _chatSO = chatSO;
            _authentication = new APIAuthentication(_chatSO.APIKey);

            _api = new OpenAIAPI(_authentication);
            _api.ApiUrlFormat = _chatSO.Url;
            _chat = _api.Chat.CreateConversation();
            _chat.Model.ModelID = _chatSO.ModelId;
            _chat.RequestParameters.Temperature = _chatSO.Temperature;
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
    }
}
