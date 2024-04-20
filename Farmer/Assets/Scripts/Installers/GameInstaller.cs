using Scripts.GameMenuCode;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<InputService>().AsSingle();
            Container.Bind<GameMenu>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}
