using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "ItemsDataBase", menuName = "SO/DataBases/Item")]
    public class InventoryItemDataBase :
        DataBase<InventoryItem>
    {
    }
}
