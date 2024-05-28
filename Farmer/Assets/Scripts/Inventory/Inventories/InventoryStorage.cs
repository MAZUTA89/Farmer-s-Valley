using Scripts.SO.Inventory;
using UnityEngine.EventSystems;
using Zenject;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public class InventoryStorage : InventoryBase, IPointerEnterHandler, IPointerExitHandler
    {
        public static bool IsMouseStay {  get; private set; }
        [Inject]
        public virtual void ConstructStorage(
            [Inject(Id = "InventoryStorageInfo")] InventoryInfo inventoryInfo,
            IInventoryCellFactory inventoryCellFactory)
        {
            TotalSize = inventoryInfo.TotalSize;
            _inventoryCellFactory = inventoryCellFactory;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            
        }
        protected override void OnDisable()
        {
            base.OnDisable();
          
        }
        protected override void OnEndDrag()
        {
            base.OnEndDrag();
        }
        public override void OnDragInto(InventoryCell inventoryCell)
        {
            base.OnDragInto(inventoryCell);
            inventoryCell.InventoryItem.IsSelected = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(" OnPointerEnter");
             IsMouseStay = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log(" OnPointerExit");
            IsMouseStay = false;
        }
        public virtual void Update()
        {
            
        }
    }
}
