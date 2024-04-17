using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using UnityEngine.Tilemaps;
using Assets.Scripts.Placement;

namespace Scripts.PlacementCode
{
    public abstract class PlacementItem : MonoBehaviour 
    {
        protected ItemPlacementMap PlacementMap;
        public Vector2Int PlacePosition {  get; private set; }

        [Inject]
        public void ConstructItem(ItemPlacementMap placementMap)
        {
            PlacementMap = placementMap;
        }
        public virtual void InitializePosition(Vector2Int placePosition)
        {
            PlacePosition = placePosition;
        }
        protected virtual void Start()
        {
            OccupyCells();
        }
        private void OccupyCells()
        {
            if(this is IOccupyingСells)
            {
                if (this is IOccupyingSeveralCells)
                {
                    IOccupyingSeveralCells obj =
                        (this as IOccupyingSeveralCells);
                    PlacementMap.AddPositions(obj);
                }
                if (this is IOccupyingOneCell)
                {
                    IOccupyingOneCell obj =
                        (this as IOccupyingOneCell);
                    PlacementMap.AddPosition(obj);
                }
            }
        }
    }
}
