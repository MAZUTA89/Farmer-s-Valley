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
        private List<Vector3Int> _sandPositions;
        private Tilemap _tileMap;

        public ItemPlacementMap(Tilemap tileMap)
        {
            _occupiedPositions = new List<Vector3Int>();
            _tileMap = tileMap;
        }
        public bool IsOccupied(Vector3Int position)
        {
            return _occupiedPositions.Contains(position);
        }
        public bool IsOccupied(List<Vector3> positions)
        {
            List<Vector3Int> intPositions = new List<Vector3Int>();
            foreach (var pos in positions)
            {
                var intPos = Vector3ConvertToVector2Int(pos);
                intPositions.Add(intPos);
            }
            foreach (var position in intPositions)
            {
                if (_occupiedPositions.Contains(position))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsOccupiedBySand()
        {
            return true;
        }

        public void AddPosition(IOccupyingOneCell obj)
        {
            _occupiedPositions.Add(obj.GetOccupyingCell());
        }
        public void AddPositions(IOccupyingSeveralCells obj)
        {
            _occupiedPositions.AddRange(obj.GetOccupyingCells());
        }
        public void RemovePosition(IOccupyingOneCell obj)
        {
            _occupiedPositions.Remove(obj.GetOccupyingCell());
        }
        public void RemovePositions(IOccupyingSeveralCells obj)
        {
            List<Vector3Int> positions = obj.GetOccupyingCells();
            _occupiedPositions.RemoveAll(x => positions.Contains(x));
        }
        public Vector3Int Vector3ConvertToVector2Int(Vector3 position)
        {
            return _tileMap.WorldToCell(position);
        }
        public List<Vector3Int> Vector3ConvertToVector2Int(List<Vector3> position)
        {
            List<Vector3Int> intPositions = new List<Vector3Int>();
            foreach (var pos in intPositions)
            {
                var intPos = Vector3ConvertToVector2Int(pos);
                intPositions.Add(intPos);
            }
            return intPositions;
        }

    }
}
