using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SaveLoader
{
    public  class PlacementItemData : IItemData
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
