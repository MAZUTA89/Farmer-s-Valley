using Scripts.SO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] PlayerSO PlayerSO;
        public override void InstallBindings()
        {
            Container.Bind<Movement>().AsSingle();
            Container.BindInstance(PlayerSO).AsTransient();
        }
    }
}