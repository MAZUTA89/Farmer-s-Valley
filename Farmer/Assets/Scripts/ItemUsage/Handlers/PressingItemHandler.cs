using Assets.Scripts.ItemUsage;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using Scripts.PlayerCode;
using Scripts.SO.Inventory;
using UnityEngine;
using UnityEngine.UIElements;
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
        Transform _playerTransform;
        public PressingItemHandler(ItemApplierTools itemApplierTools,
            Player player)
        {
            PlacementMapsContainer = itemApplierTools.PlacementMapsContainer;
            ItemPlacementMap = PlacementMapsContainer.ItemPlacementMap;

            DiContainer = itemApplierTools.DiContainer;
            MapClicker = itemApplierTools.MapClicker;
            _playerTransform = player.transform;
        }
        public void HandleItem(IInventoryItem inventoryItem)
        {
            if(HandleCondition(inventoryItem) )
            {
                DisplayItemApplyingOpportunity(inventoryItem);
                if (MapClicker.IsClicked(out Vector3Int clickedPosition))
                {
                    if(CheckInteractableDistance(inventoryItem, clickedPosition))
                    {
                        HandleClick(inventoryItem, clickedPosition);
                    }
                }
            }
            else
            {
                UsableItemKursorHandler.InvokeFalseConditionEvent();
                //UsableItemKursorHandler.InvokeDeactivatePlateEvent();
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
                if (MapClicker.TryGetMousePositionIfIntersect(out Vector3 position)
                    )
                {
                    Vector3Int position3Int = 
                        ItemPlacementMap.Vector3ConvertToVector3Int(position);

                    UsableItemKursorHandler.InvokeMovePlateEvent(position);

                    if(CheckInteractableDistance(inventoryItem, position))
                    {
                        if (UseCondition(inventoryItem, position3Int))
                        {
                            UsableItemKursorHandler.InvokeTrueConditionEvent(inventoryItem);
                        }
                        else
                        {
                            UsableItemKursorHandler.InvokeFalseConditionEvent();
                        }
                    }
                    else
                    {
                        UsableItemKursorHandler.InvokeFalseConditionEvent();
                    }
                }
                else
                {
                    UsableItemKursorHandler.InvokeFalseConditionEvent();
                }

            }
            else
            {
                UsableItemKursorHandler.InvokeDeactivatePlateEvent();
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
        bool CheckInteractableDistance(IInventoryItem inventoryItem, Vector3 mousePosition)
        {
            if (inventoryItem is IUsableInventoryItem item)
            {
                if (Vector3.Distance(mousePosition, _playerTransform.position) <
                    item.UseConditionSO.InteractDistance)
                {
                    return true;
                }
                else
                    return false;
            }
            else { return false; }
        }
        protected virtual void HandleClick(IInventoryItem inventoryItem, Vector3Int clickedPosition)
        {

        }
    }
}
