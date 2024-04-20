using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.PlacementCode
{
    public class SandTilePlacementMap : ItemPlacementMap
    {
        public SandTilePlacementMap(Tilemap sandTile) : base(sandTile)
        {
        }

        public bool IsOccupiedBySand(Vector3Int position)
        {
            return TileMap.HasTile(position);
        }
        public virtual void PlaceObjectOnCell(RuleTile gameObject, Vector3Int position)
        {
            TileMap.SetTile(position, gameObject);
        }
    }
}
