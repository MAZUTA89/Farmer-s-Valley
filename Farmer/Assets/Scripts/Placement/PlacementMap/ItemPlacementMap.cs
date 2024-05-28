using Assets.Scripts.Placement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.PlacementCode
{
    public class ItemPlacementMap
    {
        public List<Vector3Int> OccupiedPositions { get; private set; }
        protected Tilemap TileMap;
        public string Name;

        public ItemPlacementMap(Tilemap tileMap)
        {
            OccupiedPositions = new List<Vector3Int>();
            TileMap = tileMap;
        }
        public virtual bool IsOccupied(Vector3Int position)
        {
            return OccupiedPositions.Contains(position);
        }
        public virtual bool IsOccupied(List<Vector3Int> positions)
        {
            
            foreach (var position in positions)
            {
                if (OccupiedPositions.Contains(position))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddPosition(IOccupyingOneCell obj)
        {
            OccupiedPositions.Add(obj.GetOccupyingCell());
        }
        public void AddPosition(Vector3Int position)
        {
            OccupiedPositions.Add(position);
        }
        public void AddPositions(IOccupyingSeveralCells obj)
        {
            OccupiedPositions.AddRange(obj.GetOccupyingCells());
        }
        public void RemovePosition(IOccupyingOneCell obj)
        {
            OccupiedPositions.Remove(obj.GetOccupyingCell());
        }
        public void RemovePosition(Vector3Int position)
        {
            OccupiedPositions.Remove(position);
        }
        public void RemovePositions(IOccupyingSeveralCells obj)
        {
            List<Vector3Int> positions = obj.GetOccupyingCells();
            OccupiedPositions.RemoveAll(x => positions.Contains(x));
        }
        public virtual void PlaceObjectOnCell(GameObject gameObject, Vector3Int position)
        {
            Vector3 objPosition = TileMap.GetCellCenterWorld(position);
            //Vector3 objPosition = TileMap.CellToWorld(position);
            gameObject.transform.position = objPosition;
        }
        public Vector3 GetCellCenterWorld(Vector3Int position)
        {
            return TileMap.GetCellCenterWorld(position);
        }
        public Vector3Int Vector3ConvertToVector3Int(Vector3 position)
        {
            Vector3Int pos3 = TileMap.WorldToCell(position);
            return pos3;
        }
        public Vector3 Vector3IntConvertToVector3(Vector3Int position)
        {
            Vector3 pos3 = TileMap.CellToWorld(position);
            return pos3;
        }
    }
}
