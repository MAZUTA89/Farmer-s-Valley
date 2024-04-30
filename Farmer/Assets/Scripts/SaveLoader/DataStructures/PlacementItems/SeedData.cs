using System;
using System.Collections.Generic;

namespace Scripts.SaveLoader
{
    public class SeedData : PlacementItemData
    {
        public int GrowthStage;
        public float CurrentTime;
        public string SeedSOName;
        public SeedData()
        {
            ItemTypeName = nameof(SeedData);
        }
    }
}
