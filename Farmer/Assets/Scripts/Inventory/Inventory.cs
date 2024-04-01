using Scripts.SO.InventoryItem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode
{
    public class Inventory
    {
        List<InventoryItemSO> _inventoryItems;
        Transform _container;
        private List<InventoryCell> _cells;

        public Inventory(Transform container, List<InventoryItemSO> inventoryItems)
        {
            _inventoryItems = inventoryItems;
            _container = container;
            _cells = new List<InventoryCell>();
            InitializeCells(_cells, _container);
        }
        

        public void InitializeCells(List<InventoryCell> cells, Transform container)
        {
            foreach(Transform cell in container)
            {
                var inventoryCell = cell.gameObject.GetComponent<InventoryCell>();
                cells.Add(inventoryCell);
            }

            for (int i = 0; i < _cells.Count; i++)
            {
                if(i < _inventoryItems.Count)
                {
                    cells[i].SetEmpty(false);
                }
                else
                    cells[i].SetEmpty(true);
            }
            
        }
        
        public void Render()
        {
            foreach (var cell in _cells)
            {
                if(cell.IsEmpty == true)
                {
                    cell.gameObject.SetActive(false);
                }
                else
                {
                    cell.gameObject.SetActive(true);
                }
            }
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                _inventoryItems[i].RenderUI(_cells[i]);
            }
        }
    }
}
