using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SO.InteractableObjects
{
    [CreateAssetMenu(fileName = "SeedSO", menuName = "SO/Seeds/TreeSO")]
    public class OakSeedSO : SeedSO
    {
        const int c_cellSize = 1;
        public int CellsWidth;
        public int CellsHeight;

        public List<Vector3Int> GetCellsPosition(Vector3Int clickedPosition)
        {
            List<Vector3Int> positions = new List<Vector3Int>();
            Vector3Int startPos = new Vector3Int(
                clickedPosition.x - c_cellSize,
                clickedPosition.y + c_cellSize, 0);
            int width = startPos.x + CellsWidth;
            int height = startPos.y - CellsHeight;
            for (int i = startPos.y; i > height; i -= c_cellSize)
            {
                for (int j = startPos.x; j < width; j += c_cellSize)
                {
                    var pos = new Vector3Int(j, i, clickedPosition.z);
                    positions.Add(pos);
                }
            }
            ShowList(positions, startPos);
            return positions;
        }

        void ShowList(List<Vector3Int> pos, Vector3Int start)
        {
            Debug.Log($"Start position: {start.x} {start.y}");
            foreach (var posItem in pos)
            {
                Debug.Log($"{posItem.x} {posItem.y}");
            }
        }
    }
}
