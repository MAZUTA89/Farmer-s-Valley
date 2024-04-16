using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.PlacementMap
{
    public class ItemPlacementMap
    {
        private List<Vector2Int> _occupiedPositions;

        public ItemPlacementMap()
        {
            _occupiedPositions = new List<Vector2Int>();
        }
        public bool IsOccupied(Vector2Int position)
        {
            return _occupiedPositions.Contains(position);
        }
        public bool IsOccupied(List<Vector2Int> positions)
        {
            foreach (var position in positions)
            {
                if(_occupiedPositions.Contains(position))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddPosition(Vector2Int position)
        {
            _occupiedPositions.Add(position);
        }
        public void AddPositions(List<Vector2Int> positions)
        {
            _occupiedPositions.AddRange(positions);
        }
        public void RemovePosition(Vector2Int position)
        {
            _occupiedPositions.Remove(position);
        }
        public void RemovePositions(List<Vector2Int> positions)
        {
            foreach(var position in positions)
            {
                _occupiedPositions.Remove(position);
            }
        }
    }
}
