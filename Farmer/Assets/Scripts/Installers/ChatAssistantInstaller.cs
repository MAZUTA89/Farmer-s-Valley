using Zenject;
using UnityEngine;
using Scripts.ChatAssistant;
using Scripts.SO.Chat;

namespace Scripts.Installers
{
    public class ChatAssistantInstaller : MonoInstaller
    {
        [SerializeField] private ChatSODataBase _chatSODataBase;
        [SerializeField] private MassageSO _massageSO;
        [SerializeField] private MassagePanel _assistantMassagePanel;
        [SerializeField] private MassagePanel _playerMassagePanel;
        MassagePanelsFactories _massagePanelsFactories;
        public override void InstallBindings()
        {
            _massagePanelsFactories = new MassagePanelsFactories();
            _chatSODataBase.Init();
            Container.BindInstance(_chatSODataBase).AsSingle();
            Container.BindInstance(_massageSO).AsSingle();

            BindFactories();
            BindPanels();
        }
        
        void BindFactories()
        {
            _massagePanelsFactories.Add(MassagePanelType.Player, new MassagePanelFactory(_playerMassagePanel,
                Container));
            _massagePanelsFactories.Add(MassagePanelType.Assistant, new MassagePanelFactory(_assistantMassagePanel,
                Container));

            Container.BindInstance(_massagePanelsFactories).AsSingle();
        }
        void BindPanels()
        {
            Container.Bind<ChatPanel>().
                FromComponentInHierarchy()
                .AsTransient();
            Container.Bind<MassagePanel>().AsTransient();
        }
    }
}
