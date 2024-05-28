using UnityEngine;

namespace Scripts.SO.Inventory
{
    [CreateAssetMenu(fileName = "InventoryItem",
            menuName = "SO/InventoryItems/UseCondition")]
    public class UseConditionSO : ScriptableObject
    {
        public float InteractDistance;
        [ColorUsage(true)]
        public Color TrueConditionColor;
        [ColorUsage(true)]
        public Color FalseConditionColor;
    }
}
