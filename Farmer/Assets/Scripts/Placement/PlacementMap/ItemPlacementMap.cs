using Assets.Scripts.Placement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.PlacementCode
{
    public class ItemPlacementMap
    {
        private List<Vector3Int> _occupiedPositions;
        protected Tilemap TileMap;
        public string Name;

        public ItemPlacementMap(Tilemap tileMap)
        {
            _occupiedPositions = new List<Vector3Int>();
            TileMap = tileMap;
        }
        public virtual bool IsOccupied(Vector3Int position)
        {
            return _occupiedPositions.Contains(position);
        }
        public virtual bool IsOccupied(List<Vector3Int> positions)
        {
            
            foreach (var position in positions)
            {
                if (_occupiedPositions.Contains(position))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddPosition(IOccupyingOneCell obj)
        {
            _occupiedPositions.Add(obj.GetOccupyingCell());
        }
        public void AddPosition(Vector3Int position)
        {
            _occupiedPositions.Add(position);
        }
        public void AddPositions(IOccupyingSeveralCells obj)
        {
            _occupiedPositions.AddRange(obj.GetOccupyingCells());
        }
        public void RemovePosition(IOccupyingOneCell obj)
        {
            _occupiedPositions.Remove(obj.GetOccupyingCell());
        }
        public void RemovePosition(Vector3Int position)
        {
            _occupiedPositions.Remove(position);
        }
        public void RemovePositions(IOccupyingSeveralCells obj)
        {
            List<Vector3Int> positions = obj.GetOccupyingCells();
            _occupiedPositions.RemoveAll(x => positions.Contains(x));
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
