using Scripts.MainMenuCode;
using System;
using UnityEngine;
using Zenject;


namespace Scripts.ChatAssistant
{
    public class MassagePanelFactory : IMassagePanelFactory
    {
        MassagePanel _massagePanelTemplate;
        IInstantiator _instantiator;
        public MassagePanelFactory(MassagePanel massagePanelTemplate, IInstantiator instantiator)
        {
            _massagePanelTemplate = massagePanelTemplate;
            _instantiator = instantiator;
        }
        public MassagePanel CreateMassagePanel(Transform parent)
        {
            MassagePanel panel = _instantiator.InstantiatePrefabForComponent<MassagePanel>
                (_massagePanelTemplate, parent);
            return panel;
        }
    }
}
