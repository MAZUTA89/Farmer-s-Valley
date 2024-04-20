using UnityEngine;

namespace Scripts.SaveLoader
{
    public class PlayerData
    {
        public float X;
        public float Y;

        public void SetPosition(Vector2 pos)
        {
            X = pos.x;
            Y = pos.y;
        }
        public Vector2 GetPosition()
        {
            return new Vector2(X, Y);
        }
    }
}
