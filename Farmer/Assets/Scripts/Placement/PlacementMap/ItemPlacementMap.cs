using Assets.Scripts.Placement;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scripts.PlacementCode
{
    public class ItemPlacementMap
    {
        private List<Vector2Int> _occupiedPositions;
        protected Tilemap TileMap;

        public ItemPlacementMap(Tilemap tileMap)
        {
            _occupiedPositions = new List<Vector2Int>();
            TileMap = tileMap;
        }
        public virtual bool IsOccupied(Vector2Int position)
        {
            return _occupiedPositions.Contains(position);
        }
        public virtual bool IsOccupied(List<Vector2Int> positions)
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
        public void AddPosition(Vector2Int position)
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
        public void RemovePosition(Vector2Int position)
        {
            _occupiedPositions.Remove(position);
        }
        public void RemovePositions(IOccupyingSeveralCells obj)
        {
            List<Vector2Int> positions = obj.GetOccupyingCells();
            _occupiedPositions.RemoveAll(x => positions.Contains(x));
        }


        public virtual void PlaceObjectOnCell(GameObject gameObject, Vector3Int position)
        {
            Vector3 objPosition = TileMap.CellToWorld(position);
            gameObject.transform.position = objPosition;
        }
        
        public Vector2Int Vector3ConvertToVector2Int(Vector3 position)
        {
            Vector3Int pos3 = TileMap.WorldToCell(position);
            Vector2Int pos2 = new Vector2Int(pos3.x, pos3.y);
            return pos2;
        }
        //public List<Vector2Int> Vector3ConvertToVector2Int(List<Vector2> position)
        //{
        //    List<Vector3Int> intPositions = new List<Vector3Int>();
        //    foreach (var pos in intPositions)
        //    {
        //        var intPos = Vector3ConvertToVector2Int(pos);
        //        intPositions.Add(intPos);
        //    }
        //    return intPositions;
        //}

    }
}
