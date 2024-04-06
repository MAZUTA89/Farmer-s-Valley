using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public class StorageCellFactory : IInventoryCellFactory
    {
        InventoryCell _inventoryCellTemplate;
        GameObject _emptyCellPrefab;
        public StorageCellFactory(InventoryCell inventoryCellTemplate, GameObject emptyCellPrefab) 
        {
            _inventoryCellTemplate = inventoryCellTemplate;
            _emptyCellPrefab = emptyCellPrefab;
        }

        public InventoryCell Create(Transform visualContext)
        {
            return GameObject.Instantiate(_inventoryCellTemplate, visualContext);
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
