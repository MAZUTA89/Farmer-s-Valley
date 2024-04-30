using Scripts.SaveLoader;
using System;
using System.Collections.Generic;

namespace AScripts.SaveLoader
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
