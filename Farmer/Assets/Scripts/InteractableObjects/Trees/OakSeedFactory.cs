using Scripts.SO.InteractableObjects;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scripts.InteractableObjects
{
    internal class OakSeedFactory : ISeedFactory
    {
        DiContainer _container;
        Tree _treeTemplate;
        public OakSeedFactory(DiContainer container,
            Tree treeTemplate)
        {
            _container = container;
            _treeTemplate = treeTemplate;
        }
        public Seed Create(SeedSO createData)
        {
            var tree = _container.InstantiatePrefabForComponent<Tree>(_treeTemplate);
            tree.Initialize(createData);
            return tree;
        }
    }
}
