﻿using HappyHarvest;
using Scripts.MainMenuCode;
using Scripts.SaveLoader;
using Scripts.SellBuy;
using Scripts.Sounds;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class NextLevelInstaller : MonoInstaller
    {
        
        public override void InstallBindings()
        {
            Container.Bind<SoundService>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.Bind<BasicAnimalMovement>()
                .FromComponentInHierarchy()
                .AsTransient();
            Container.Bind<Seller>()
                .FromComponentInHierarchy()
                .AsSingle();
            SettingsMenu settingsMenu =
                new SettingsMenu(null, null, false);
            Container.BindInstance(settingsMenu).AsSingle();
            Container.Bind<InputService>().AsSingle();
        }
    }
}