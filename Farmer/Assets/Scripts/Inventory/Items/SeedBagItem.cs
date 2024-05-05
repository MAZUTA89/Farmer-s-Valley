using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "SeedBag", menuName = "SO/InventoryItems/SeedBag")]
    public class SeedBagItem : InventoryItem
    {
        
        public int CurrentCount
        {
            get
            {
                return _startCount;
            }
            set
            {
                _startCount = value;

                if(_startCount < 0)
                    _startCount = 0;
            }
        }
        [Range(0, 10)]
        [SerializeField] private int _startCount;
        public override bool Apply(Vector3Int target)
        {
            throw new NotImplementedException();
        }

        public override bool ApplyCondition(Vector3Int target)
        {
            throw new NotImplementedException();
        }
        public override void RenderUI(InventoryCell inventoryCell)
        {
            base.RenderUI(inventoryCell);
            inventoryCell.CountText.text = CurrentCount.ToString();
        }
    }
}
