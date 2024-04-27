using Scripts.InteractableObjects;
using Scripts.SO.InteractableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Tree = Scripts.InteractableObjects.Tree;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName ="OakBag", menuName = "SO/InventoryItems/OakBagInventoryItem")]
    public class OakBagInventoryItem : BagInventoryItem, IOakBagInventoryItem
    {
        
    }
}
