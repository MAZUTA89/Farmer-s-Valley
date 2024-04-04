
using Scripts.SO.InventoryItem;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] int Size;
        private Transform _container;
        private RectTransform _rectTransform;
        private List<InventoryItemAssetData> _inventoryItemsAssetData;
        private List<InventoryItem> _inventoryItems;
        private List<InventoryCell> _cells;
        private Transform _dragParent;


        [Inject]
        public void Construct([Inject(Id = "DragParent")] Transform dragParent)
        {
            _dragParent = dragParent;
        }

        private void Start()
        {
            _container = gameObject.transform;
            _rectTransform = _container.GetComponent<RectTransform>();
            EndDragExtension.RegisterInventoryRectTransform
                (_rectTransform);

            _inventoryItems = new List<InventoryItem>(Size);
            _cells = new List<InventoryCell>();
            InitializeInventoryItems(_inventoryItemsAssetData);
            InitializeCells(_cells, _container);
        }
        private void OnDisable()
        {
            EndDragExtension.UnregisterInventoryRectTransform
                (_rectTransform);
        }
        
        private void Update()
        {
            Render();
        }
        public void InitializeItemKit(List<InventoryItemAssetData> inventoryItemAssetDatas)
        {
            _inventoryItemsAssetData = inventoryItemAssetDatas;
        }
        void InitializeCells(List<InventoryCell> cells,
            Transform container)
        {
            foreach(Transform cellObject in container)
            {
                var inventoryCell = 
                    cellObject.gameObject.GetComponent<InventoryCell>();
                cells.Add(inventoryCell);
            }
            foreach(var cell in cells)
            {
                cell.InitializeDragParent(_dragParent);
            }

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                _cells[i].InitializeItem(_inventoryItems[i]);
                _cells[i].InitializeDragEvent(OnEndDragItem);
            }

            for (int i = 0; i < _cells.Count; i++)
            {
                if (i < _inventoryItems.Count)
                {
                    cells[i].SetEmpty(false);
                }
                else
                    cells[i].SetEmpty(true);
            }

        }
        void InitializeInventoryItems(
            List<InventoryItemAssetData> inventoryItemAssetDatas)
        {

            foreach (var assetData in inventoryItemAssetDatas)
            {
                InventoryItem inventoryItem = null;
                switch (assetData)
                {
                    case QuantitativeItemAssetData quantitativeItem:
                        {
                            inventoryItem = new QuantitativeItem(quantitativeItem);
                            break;
                        }
                    case InventoryItemAssetData inventoryItemAssetData:
                        {
                            inventoryItem = new ToolItem(inventoryItemAssetData);
                            break;
                        }
                }
                _inventoryItems.Add(inventoryItem);
            }
        }
        public void OnEndDragItem()
        {
            OverwriteCellsList(_cells, _container);
            OverwriteInventoryItems(_inventoryItems);
        }
        void OverwriteCellsList(List<InventoryCell> inventoryCells,
            Transform container)
        {
            int i = 0;
            foreach (Transform cell in container)
            {
                var inventoryCell = cell.gameObject.GetComponent<InventoryCell>();
                inventoryCells[i] = inventoryCell;
                i++;
            }
        }
        void OverwriteInventoryItems(List<InventoryItem> inventoryItems)
        {
            for (int i = 0; i < _container.childCount; i++)
            {
                Transform child = _container.GetChild(i);
                var inventoryCell =
                    child.gameObject.GetComponent<InventoryCell>();
                if (inventoryCell.InventoryItem != null)
                {
                    inventoryItems[i] = inventoryCell.InventoryItem;
                }
            }
        }
        public void Render()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                foreach (var item in _inventoryItems)
                {
                    Debug.Log("Item: " + item.Name);
                }
            }
            foreach (var cell in _cells)
            {
                if (cell.IsEmpty == true)
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

        public void AddCell(InventoryCell inventoryCell)
        {
            inventoryCell.InitializeDragEvent(OnEndDragItem);
            inventoryCell.OverwriteDragOrigin(_container);
            _cells.Add(inventoryCell);
        }
    }
}
