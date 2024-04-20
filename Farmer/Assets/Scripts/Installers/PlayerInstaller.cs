using Scripts.SO;
using Scripts.SO.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] PlayerSO PlayerSO;
        [SerializeField] Transform _playerTransform;
        public override void InstallBindings()
        {
            Container.BindInstance(_playerTransform)
                .WithId("PlayerTransform")
                .AsTransient();
            Container.Bind<Movement>().FromComponentInHierarchy()
                .AsSingle();
            Container.BindInstance(PlayerSO).AsTransient();
        }
    }
}