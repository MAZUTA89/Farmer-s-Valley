using UnityEngine;

namespace Scripts.SaveLoader
{
    public  class PlacementItemData : IItemData
    {
        public int X;
        public int Y;
        public string ItemTypeName;

        public PlacementItemData()
        {
            ItemTypeName = nameof(PlacementItemData);
        }
        public void SetPosition(Vector2Int position)
        {
            X = position.x;
            Y = position.y;
        }
        public Vector2Int GetPosition()
        {
            return new Vector2Int(X, Y);
        }
    }
}
