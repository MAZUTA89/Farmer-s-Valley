using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.SaveLoader.DataStructures
{
    public class PlacementItemData : IItemData
    {
        public int X;
        public int Y;
        
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
