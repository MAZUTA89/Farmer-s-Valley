﻿using PimDeWitte.UnityMainThreadDispatcher;
using Scripts.SO.Chat;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Assets.Scripts.GameSO.Chat;

namespace Scripts.ChatAssistant
{
    public class ChatPanel : MonoBehaviour
    {
        [SerializeField] private string Name;
        Transform _container;
        MassagePanelsFactories _massagePanelsFactories;

        ChatService _chatService;
        ChatContextOptimizer _chatContextOptimizer;

        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _enterButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private GameObject _chatPanel;

        InputService _inputService;
        ChatSODataBase _chatSOData;


        [Inject]
        public void Construct(MassagePanelsFactories massagePanelsFactories,
            ChatSODataBase chatSODataBase,
            InputService inputService)
        {
            _chatSOData = chatSODataBase;
            _massagePanelsFactories = massagePanelsFactories;
            _inputService = inputService;
        }

        private void Start()
        {
            ChatSO chatSO = _chatSOData.GetItemByName(Name);
            
            if(chatSO == null)
            {
                Debug.LogError($"Cannot get chat SO Object by name {Name}");
                Destroy(gameObject);
            }
            else
            {
                _chatService = new ChatService(chatSO);

                _chatContextOptimizer = new ChatContextOptimizer(chatSO.EncodingModelID,
                    chatSO.ResetContextSize);

                if(_chatContextOptimizer.TryInitialize() == false)
                {
                    Debug.LogError($"Cannot initialize chat context optimizer by name {Name}");
                    Destroy(gameObject);
                }
            }
            _container = gameObject.transform;
            _resetButton.interactable = false;
        }

        

        public async Task CreateMassagePanelAsync(IMassagePanelFactory massagePanelFactory, string text)
        {

            MassagePanel massagePanel = massagePanelFactory.CreateMassagePanel(_container);

            ResizeMassagePanel(massagePanel, text);

            await massagePanel.WriteTextAsync(text);
        }
        public void CreateMassagePanel(IMassagePanelFactory massagePanelFactory, string text)
        {

            MassagePanel massagePanel = massagePanelFactory.CreateMassagePanel(_container);

            ResizeMassagePanel(massagePanel, text);

            massagePanel.WriteText(text);
        }

        void ResizeMassagePanel(MassagePanel massagePanel, string text)
        {
            massagePanel.TextField.text = text;

            massagePanel.TextField.ForceMeshUpdate();

            var size = massagePanel.TextField.GetPreferredValues();

            float panelHeight = size.y;

            massagePanel.Size = new Vector2(massagePanel.Size.x, panelHeight + massagePanel.Size.y);
        }

        public async void OnEnterText()
        {
            if (_inputField.text.Length > 0)
            {
                IMassagePanelFactory playerPanelFactory = _massagePanelsFactories.GetByType(MassagePanelType.Player);

                CreateMassagePanel(playerPanelFactory, _inputField.text);

                _enterButton.interactable = false;

                _chatService.AddUserInput(_inputField.text);

                _inputField.text = String.Empty;
                await ConstructResponse();
            }

        }
        public void OnReset()
        {
            _chatService.ClearChat();
            ClearMassagePanels();
            _enterButton.interactable = true;
            _resetButton.interactable = false;
        }

        async Task ConstructResponse()
        {
            await Task.Run(() =>
            {
                UnityMainThreadDispatcher.Instance().Enqueue(async () =>
                {
                    string text = "";
                    IMassagePanelFactory assistantPanelFactory = _massagePanelsFactories.GetByType(MassagePanelType.Assistant);
                    try
                    {
                        text = await _chatService.GetResponseAsync();
                    }
                    catch (Exception ex)
                    {
                        text += ex.Message;
                    }
                    finally
                    {
                        await CreateMassagePanelAsync(assistantPanelFactory, text)
                           .ContinueWith((t) =>
                           {
                               UnityMainThreadDispatcher.Instance().Enqueue(() =>
                               {
                                   _enterButton.interactable = true;
                                   Debug.Log("End cunstruct response!");

                                   if(_chatContextOptimizer.IsNeedReset(_chatService.GetContextText()))
                                   {
                                       _enterButton.interactable = false;
                                       _resetButton.interactable = true;
                                   }
                               });
                           });
                    }
                });
            });
        }

        public void OnExit()
        {
            _inputService.UnlockGamePlayControls();
            _chatPanel.SetActive(false);
        }

        void ClearMassagePanels()
        {
            foreach(Transform massagePanel in _container)
            {
                Destroy(massagePanel.gameObject);
            }
        }

    }
}
