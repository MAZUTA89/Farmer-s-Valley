using Scripts.InventoryCode;
using Scripts.SaveLoader;
using UnityEngine;

namespace Assets.Scripts.Inventory.Items
{
    [CreateAssetMenu(fileName = "HoeInventoryItem",
            menuName = "SO/InventoryItems/HoeInventoryItem")]
    public class HoeInventoryItem : UsableInventoryItem, IHoeInventoryItem
    {
        public RuleTile ProductionObject => _sandTile;
        [SerializeField] private RuleTile _sandTile;

        public override InventoryItemData GetItemData()
        {
           return base.GetItemData();
        }
    }
}
