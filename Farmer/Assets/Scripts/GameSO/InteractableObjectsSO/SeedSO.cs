using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.SO.InteractableObjects
{
    [CreateAssetMenu(fileName ="SeedSO", menuName = "SO/Seeds/SeedSO")]
    public class SeedSO : ScriptableObject
    {
        public List<Sprite> StagesSpriteList;
        public float IntervalTimeBetweenGrowthStages;
    }
}
