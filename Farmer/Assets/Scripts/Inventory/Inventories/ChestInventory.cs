using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Scripts.InventoryCode
{
    public class ChestInventory : InventoryStorage
    {
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            transform.position = Input.mousePosition;
        }
    }
}
