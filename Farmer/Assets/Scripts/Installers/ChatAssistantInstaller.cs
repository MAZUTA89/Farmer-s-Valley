using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.ChatAssistant;
using Scripts.SO.Chat;

namespace Scripts.Installers
{
    public class ChatAssistantInstaller : MonoInstaller
    {
        [SerializeField] private ChatSO _chatSO;
        [SerializeField] private MassageSO _massageSO;
        [SerializeField] private MassagePanel _assistantMassagePanel;
        [SerializeField] private MassagePanel _playerMassagePanel;
        MassagePanelsFactories _massagePanelsFactories;
        public override void InstallBindings()
        {
            Container.BindInstance(_chatSO).AsSingle();
            Container.BindInstance(_massageSO).AsSingle();
            Container.Bind<ChatService>().AsSingle();
            BindFactories();
        }

        void BindFactories()
        {
            _massagePanelsFactories = new MassagePanelsFactories();

            _massagePanelsFactories.Add(MassagePanelType.Player, new MassagePanelFactory(_playerMassagePanel,
                Container));
            _massagePanelsFactories.Add(MassagePanelType.Assistant, new MassagePanelFactory(_assistantMassagePanel,
                Container));

            Container.BindInstance(_massagePanelsFactories).AsSingle();
        }
    }
}
