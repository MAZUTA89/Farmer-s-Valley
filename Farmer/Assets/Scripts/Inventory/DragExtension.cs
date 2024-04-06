using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Scripts.InventoryCode;
using UnityEngine.UI;
using System.Threading.Tasks;


namespace Scripts.InventoryCode
{
    internal static class DragExtension
    {
        static List<RectTransform> _containersRectTransform;
        static readonly int c_MoveCellSpeed;
        static DragExtension()
        {
            if (_containersRectTransform == null)
            {
                _containersRectTransform = new List<RectTransform>();
            }
            c_MoveCellSpeed = 000000001;
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
        public static async void PlaceInTheNearestCellLocal(Transform visualContext,
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
        }
        public static Transform GetNearestCellTransform(Transform visualContext,
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
            return visualContext.GetChild(closetIndex);
        }

        static async Task MoveCellTo(Transform cellTransform, int closetIndex, int currentIndex)
        {
            if(closetIndex > currentIndex)
            {
                for (int i = currentIndex; i <= closetIndex; i++)
                {
                    cellTransform.SetSiblingIndex(i);
                   await Task.Delay(c_MoveCellSpeed);
                }
            }
            else
            {
                for(int i = currentIndex; i >= closetIndex; i--)
                {
                    cellTransform.SetSiblingIndex(i);
                    await Task.Delay(c_MoveCellSpeed);
                }
            }
            
        }
        static async void Replace(InventoryCell inventoryCell1, InventoryCell inventoryCell2)
        {
            inventoryCell1.transform.SetParent(inventoryCell2.OriginVisualContext);
            inventoryCell2.transform.SetParent(inventoryCell1.OriginVisualContext);
            await MoveCellTo(inventoryCell1.transform, inventoryCell2.BeginDragSiblingIndex, inventoryCell1.transform.GetSiblingIndex());
            await MoveCellTo(inventoryCell2.transform, inventoryCell1.BeginDragSiblingIndex, inventoryCell2.transform.GetSiblingIndex());
        }
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
    }
}
