using UnityEngine;

namespace Scripts.SaveLoader
{
    public class PlacementItemData 
    {
        public int X;
        public int Y;
        public string ItemTypeName;

        public PlacementItemData()
        {
            ItemTypeName = nameof(PlacementItemData);
        }
        public void SetPosition(Vector3Int position)
        {
            X = position.x;
            Y = position.y;
        }
        public Vector3Int GetPosition()
        {
            return new Vector3Int(X, Y);
        }
    }
}
