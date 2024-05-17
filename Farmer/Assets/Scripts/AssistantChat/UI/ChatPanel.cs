using PimDeWitte.UnityMainThreadDispatcher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.ChatAssistant
{
    public class ChatPanel : MonoBehaviour
    {
        Transform _container;
        MassagePanelsFactories _massagePanelsFactories;
        ChatService _chatService;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _enterButton;
        [SerializeField] private GameObject _chatPanel;

        InputService _inputService;

        [Inject]
        public void Construct(MassagePanelsFactories massagePanelsFactories,
            ChatService chatService,
            InputService inputService)
        {
            _massagePanelsFactories = massagePanelsFactories;
            _chatService = chatService;
            _inputService = inputService;
        }

        private void Start()
        {
            _container = gameObject.transform;
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

    }
}
