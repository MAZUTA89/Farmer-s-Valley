using System;
using System.Collections.Generic;
using UnityEngine;
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
            (PointerEventData eventData, out Inventory inventory)
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
                    inventory = rect.gameObject.GetComponent<Inventory>();
                    return true;
                }

            }
            inventory = null;
            return false;
        }
    }
}
