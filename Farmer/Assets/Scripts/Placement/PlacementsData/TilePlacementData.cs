using UnityEngine;

namespace Scripts.PlacementCode
{
    public class TilePlacementData
    {
        public int X;
        public int Y;
        public int Z;
        public void SetPosition(Vector3Int position)
        {
            X = position.x;
            Y = position.y;
            Z = position.z;
        }
        public Vector3Int GetPosition()
        {
            return new Vector3Int(X, Y, Z);
        }
    }
}
