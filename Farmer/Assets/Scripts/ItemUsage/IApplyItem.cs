using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Installers
{
    internal interface IApplyItem
    {
        void ApplyItem(InventoryItem inventoryItem);
    }
}
