using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "Crop", menuName = "SO/InventoryItems/Crop")]
    public class Crop : ScriptableObject,
        IDataBaseItem
    {
        public string UniqueName => _name;
        [SerializeField] private string _name;

        public TileBase[] GrowthStagesTiles;

        public ProductItem Produce;

        public float GrowthTime = 1.0f;
        public int NumberOfHarvest = 1;
        public int StageAfterHarvest = 1;
        public int ProductPerHarvest = 1;
        public float DryDeathTimer = 30.0f;
        public VisualEffect PickEffect;

        public int GetGrowthStage(float growRatio)
        {
            return Mathf.FloorToInt(growRatio * (GrowthStagesTiles.Length - 1));
        }
    }
}
