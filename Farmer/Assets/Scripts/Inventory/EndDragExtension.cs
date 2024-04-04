using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Scripts.InventoryCode;
using UnityEngine.UI;


namespace Scripts.InventoryCode
{
    internal static class EndDragExtension
    {
        static List<RectTransform> _containersRectTransform;
        static EndDragExtension()
        {
            if (_containersRectTransform == null)
            {
                _containersRectTransform = new List<RectTransform>();
            }
        }
        public static void RegisterInventoryRectTransform
            (RectTransform containerRectTransform)
        {
            _containersRectTransform.Add(containerRectTransform);
        }
        public static void UnregisterInventoryRectTransform
            (RectTransform containerRectTransform)
        {
            _containersRectTransform.Remove(containerRectTransform);
        }

        public static bool CheckMouseIntersectionWithContainers
            (PointerEventData eventData, out InventoryBase inventory)
        {
            foreach (var rect in _containersRectTransform)
            {
                Vector2 mousePosition;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rect,
                    eventData.position,
                    eventData.pressEventCamera,
                    out mousePosition);

                if(rect.rect.Contains(mousePosition))
                {
                    inventory = rect.gameObject.GetComponent<InventoryBase>();
                    return true;
                }

            }
            inventory = null;
            return false;
        }
        public static void PlaceInTheNearestCell(Transform visualContext,
            InventoryCell inventoryCell)
        {
            int closetIndex = 0;
            for (int i = 0; i < visualContext.transform.childCount; i++)
            {
                if (Vector3.Distance(visualContext.GetChild(i).position,
                    inventoryCell.transform.position) <
                    Vector3.Distance(visualContext.GetChild(closetIndex).position,
                    inventoryCell.transform.position))
                {
                    closetIndex = i;
                }
            }
            inventoryCell.transform.SetParent(visualContext);
            inventoryCell.transform.SetSiblingIndex(closetIndex);
        }

        
    }
}
