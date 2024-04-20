using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class StatePanelFactory : IGameStatePanelFactory
    {
        DiContainer _container;

        GameStatePanel _template;
        public StatePanelFactory(
            DiContainer container,
            [Inject(Id = "StatePanelTemplate")] GameStatePanel gameStatePanel)
        {
            _container = container;
            _template = gameStatePanel;
        }
        public GameStatePanel Create(string stateName, Transform content)
        {
            //var panel = _container
            //    .InstantiatePrefabForComponent<GameStatePanel>
            //    (_template, content);
            var panel = GameObject.Instantiate(_template,
                content);
            panel.NameText.text = stateName;
            return panel;
        }
    }
}

