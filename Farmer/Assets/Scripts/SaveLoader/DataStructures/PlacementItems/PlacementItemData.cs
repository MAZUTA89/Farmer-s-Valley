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
        public override bool Equals(object obj)
        {
            if(obj is PlacementItemData)
            {
                PlacementItemData other = (PlacementItemData)obj;
                if (GetType() == other.GetType())
                {
                    if (other.GetPosition() == GetPosition())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else return false;
            }
            else { return false; }
        } 
    }
}
