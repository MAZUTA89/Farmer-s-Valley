using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Scripts.InventoryCode;
using UnityEngine.UI;
using System.Threading.Tasks;


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
        public static async void PlaceInTheNearestCell(Transform visualContext,
            InventoryCell inventoryCell, int currentIndex)
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
            await MoveCellTo(inventoryCell.transform, closetIndex, currentIndex);
            //inventoryCell.transform.SetSiblingIndex(closetIndex);
        }
        //public static void PlaceInTheNearestCell(Transform visualContext, InventoryCell inventoryCell)
        //{
        //    int closestIndex = 0;
        //    float closestDistance = 0;
        //    Vector3 localPosition = visualContext.InverseTransformPoint(inventoryCell.transform.position);

        //    for (int i = 0; i < visualContext.childCount; i++)
        //    {
        //        float distance = Vector3.Distance(localPosition, visualContext.GetChild(i).localPosition);
        //        if (distance < closestDistance)
        //        {
        //            closestDistance = distance;
        //            closestIndex = i;
        //        }
        //    }

        //    inventoryCell.transform.SetParent(visualContext);
        //    inventoryCell.transform.SetSiblingIndex(closestIndex);
        //}

        public static void ShowNearestCellFor(InventoryCell inventoryCell, Transform visualContext)
        {
            int closetIndex = 0;
            Debug.Log("Count " + visualContext.transform.childCount);
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
           var child = visualContext.GetChild(closetIndex);
            Debug.Log(closetIndex);
        }
        static async Task  MoveCellTo(Transform cellTransform, int closetIndex, int currentIndex)
        {
            //int currentIndex = cellTransform.GetSiblingIndex();
            if(closetIndex > currentIndex)
            {
                for (int i = currentIndex; i <= closetIndex; i++)
                {
                    cellTransform.SetSiblingIndex(i);
                    await Task.Delay(500);
                }
            }
            else
            {
                for(int i = currentIndex; i >= closetIndex; i--)
                {
                    cellTransform.SetSiblingIndex(i);
                    await Task.Delay(500);
                }
            }
            
        }
    }
}
