using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.PlacementCode
{
    public class SandTilePlacementMap : ItemPlacementMap
    {

        public SandTilePlacementMap(Tilemap sandTile)
            : base(sandTile)
        {
        }

        public bool IsOccupiedBySand(Vector3Int position)
        {
            return TileMap.HasTile(position);
        }
        public bool IsOccupiedBySand(List<Vector3Int> positions)
        {
            foreach (var position in positions)
            {
                if (TileMap.HasTile(position))
                {
                    return true;
                }
            }
            return false;
        }
        public virtual void PlaceObjectOnCell(RuleTile gameObject, Vector3Int position)
        {
            TileMap.SetTile(position, gameObject);
        }
    }
}
