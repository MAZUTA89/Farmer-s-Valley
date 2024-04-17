using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.PlacementCode;

namespace Scripts.Installers
{
    public class GameStateInstaller : MonoInstaller
    {
        [Header("Дефолтные объекты на уровне")]
        [SerializeField] private List<PlacementItem> _placementItems;
        public override void InstallBindings()
        {
            
        }
    }
}
