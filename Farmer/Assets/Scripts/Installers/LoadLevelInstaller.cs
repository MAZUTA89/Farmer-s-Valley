using System;
using Zenject;
using UnityEngine;
using Assets.Scripts.LevelsLoading;

namespace Scripts.Installers
{
    public class LoadLevelInstaller : MonoInstaller
    {
        [SerializeField] private LoadScreenPanel _loadScreenPanel;
        
        public override void InstallBindings()
        {
            LevelLoader levelLoader = new LevelLoader(_loadScreenPanel);
            _loadScreenPanel.gameObject.SetActive(false);

            Container.BindInstance(levelLoader).AsSingle();
        }
    }
}
