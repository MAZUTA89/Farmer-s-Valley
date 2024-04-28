using Assets.Scripts.ItemUsage;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using Scripts.SO.Inventory;
using UnityEngine;
using Zenject;


namespace Scripts.ItemUsage
{
    public abstract class PressingItemHandler : IItemHandler
    {
        public IItemHandler Successor { set; get; }
        protected PlacementMapsContainer PlacementMapsContainer;
        protected DiContainer DiContainer;
        protected MapClicker MapClicker;
        private GameObject _kursorObject;
        private SpriteRenderer _sr;
        public PressingItemHandler(ItemApplierTools itemApplierTools)
        {
             PlacementMapsContainer = itemApplierTools.PlacementMapsContainer;
            DiContainer = itemApplierTools.DiContainer;
            MapClicker = itemApplierTools.MapClicker;
            _kursorObject = itemApplierTools.KursorObject;
            _sr = _kursorObject.GetComponent<SpriteRenderer>();
        }
        public virtual void HandleItem(IInventoryItem inventoryItem)
        {
            if(inventoryItem is IUsableInventoryItem usableInventoryItem)
            {
                UseConditionSO useConditionSO = usableInventoryItem.UseConditionSO;

                _kursorObject.SetActive(true);

                if (MapClicker.TryGetMousePositionIfIntersect(out Vector3 position))
                {
                    _kursorObject.transform.position = position;
                    if (UseCondition(inventoryItem, position))
                    {
                        _sr.color = useConditionSO.TrueConditionColor;
                    }
                    else
                    {
                        _sr.color = useConditionSO.FalseConditionColor;
                    }
                }
                else
                {
                    _sr.color = useConditionSO.FalseConditionColor;
                }
                
            }
            else
            {
                _kursorObject.SetActive(false);
            }

        }

        protected virtual bool UseCondition(IInventoryItem inventoryItem,
            Vector3 position)
        {
            return false;
        }
    }
}
