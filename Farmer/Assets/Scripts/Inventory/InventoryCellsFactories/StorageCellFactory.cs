using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode
{
    public class StorageCellFactory : IInventoryCellFactory
    {
        InventoryCell _inventoryCellTemplate;
        GameObject _emptyCellPrefab;
        DiContainer _container;
        public StorageCellFactory(DiContainer diContainer,
            InventoryCell inventoryCellTemplate,
            GameObject emptyCellPrefab) 
        {
            _inventoryCellTemplate = inventoryCellTemplate;
            _emptyCellPrefab = emptyCellPrefab;
            _container = diContainer;
        }

        public InventoryCell Create(Transform visualContext)
        {
            return _container.InstantiatePrefabForComponent<InventoryCell>
                (_inventoryCellTemplate, visualContext);
            //return GameObject.Instantiate(_inventoryCellTemplate, visualContext);
        }

        public GameObject CreateEmpty(InventoryCell inventoryCell)
        {
            int siblingIndex = inventoryCell.transform.GetSiblingIndex();
            var cell = GameObject.Instantiate(_emptyCellPrefab, inventoryCell.OriginVisualContext);
            cell.name = "SiblingCell";
            cell.transform.SetSiblingIndex(siblingIndex);
            return cell;
        }
    }
}
