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
        protected ItemPlacementMap ItemPlacementMap;
        protected DiContainer DiContainer;
        protected MapClicker MapClicker;
        private GameObject _kursorObject;
        private SpriteRenderer _sr;
        public PressingItemHandler(ItemApplierTools itemApplierTools)
        {
            PlacementMapsContainer = itemApplierTools.PlacementMapsContainer;
            ItemPlacementMap = PlacementMapsContainer.ItemPlacementMap;

            DiContainer = itemApplierTools.DiContainer;
            MapClicker = itemApplierTools.MapClicker;
            _kursorObject = itemApplierTools.KursorObject;
            _sr = _kursorObject.GetComponent<SpriteRenderer>();
        }
        public void HandleItem(IInventoryItem inventoryItem)
        {
            DisplayItemApplyingOpportunity(inventoryItem);

            if(HandleCondition(inventoryItem))
            {
                if(MapClicker.IsClicked(out Vector3Int clickedPosition))
                {
                    HandleClick(inventoryItem, clickedPosition);
                }
            }
            else
            {
                Successor?.HandleItem(inventoryItem);
            }

        }
        /// <summary>
        /// Отображение курсора при выборе предмета
        /// </summary>
        /// <param name="inventoryItem"></param>
        void DisplayItemApplyingOpportunity(IInventoryItem inventoryItem)
        {
            if (inventoryItem is IUsableInventoryItem usableInventoryItem)
            {
                UseConditionSO useConditionSO = usableInventoryItem.UseConditionSO;

                _kursorObject.SetActive(true);

                if (MapClicker.TryGetMousePositionIfIntersect(out Vector3 position))
                {
                    _kursorObject.transform.position = position;
                    Vector3Int position3Int = 
                        ItemPlacementMap.Vector3ConvertToVector3Int(position);
                    if (UseCondition(inventoryItem, position3Int))
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
        /// <summary>
        /// При каком условии обрабатывать элемент последовательности
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <returns></returns>
        protected virtual bool HandleCondition(IInventoryItem inventoryItem)
        {
            return false;
        }
        /// <summary>
        /// Условие, когда можно использовать предмет
        /// </summary>
        /// <param name="inventoryItem"></param>
        /// <param name="position">позиция мыши на карте в моровых координатах</param>
        /// <returns></returns>
        protected virtual bool UseCondition(IInventoryItem inventoryItem,
            Vector3Int position)
        {
            return false;
        }

        protected virtual void HandleClick(IInventoryItem inventoryItem, Vector3Int clickedPosition)
        {

        }
    }
}
