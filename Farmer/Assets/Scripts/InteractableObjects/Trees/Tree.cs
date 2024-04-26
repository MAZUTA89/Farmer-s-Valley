using Assets.Scripts.Placement;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InteractableObjects
{
    public class Tree : Seed, IOccupyingSeveralCells
    {
        public List<Vector3Int> GetOccupyingCells()
        {
            List<Vector3Int> positions = new List<Vector3Int>();
            positions.Add(PlacePosition);
            return positions;


        }
    }
}
