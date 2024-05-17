using UnityEngine;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class StatePanelFactory : IGameStatePanelFactory
    {

        GameStatePanel _template;
        public StatePanelFactory(
            [Inject(Id = "StatePanelTemplate")] GameStatePanel gameStatePanel)
        {
            _template = gameStatePanel;
        }
        public GameStatePanel Create(string stateName, Transform content)
        {
            var panel = GameObject.Instantiate(_template,
                content);
            panel.NameText.text = stateName;
            return panel;
        }
    }
}

