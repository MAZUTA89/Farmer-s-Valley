﻿using Scripts.SO.InteractableObjects;
using Zenject;
using UnityEngine;

namespace Scripts.InteractableObjects
{
    public class SeedFactory : ISeedFactory
    {
        DiContainer _container;
        Seed _seedTemplate;
        public SeedFactory(DiContainer diContainer,
            Seed seedTemplate) 
        {
            _container = diContainer;
            _seedTemplate = seedTemplate;
        }
        public Seed Create(SeedSO createData)
        {
            var seed = _container.InstantiatePrefabForComponent<Seed>(
                _seedTemplate);
            seed.Initialize(createData);
            return seed;
        }
    }
}
