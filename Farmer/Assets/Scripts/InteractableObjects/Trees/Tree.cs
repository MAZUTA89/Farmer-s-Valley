using AScripts.SaveLoader;
using Assets.Scripts.Placement;
using Scripts.PlacementCode;
using Scripts.SaveLoader;
using Scripts.SO.InteractableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InteractableObjects
{
    public class Tree : Seed, IOccupyingSeveralCells
    {
        public override void ConstructItem(PlacementMapsContainer placementMapsContainer, GameDataState gameDataState)
        {
            GameDataState = gameDataState;
            PlacementMap = placementMapsContainer.ItemPlacementMap;
        }
        public List<Vector3Int> GetOccupyingCells()
        {
            return (SeedSO as OakSeedSO).GetCellsPosition(PlacePosition);
        }
        public override PlacementItemData GetData()
        {
            SeedData data = (SeedData)base.GetData();
            TreeData treeData = new TreeData()
            {
                SeedSOName = data.SeedSOName,
                GrowthStage = data.GrowthStage,
                CurrentTime = data.CurrentTime,
            };
            treeData.SetPosition(data.GetPosition());
            return treeData;
        }
    }
}
