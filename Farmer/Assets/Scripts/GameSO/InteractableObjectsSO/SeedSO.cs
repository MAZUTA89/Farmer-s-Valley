using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.SO.InteractableObjects
{
    public class SeedSO : ScriptableObject
    {
        public int GrowsStages;
        public List<Sprite> StagesSpriteList;
        public float IntervalTimeBetweenGrowthStages;
    }
}
