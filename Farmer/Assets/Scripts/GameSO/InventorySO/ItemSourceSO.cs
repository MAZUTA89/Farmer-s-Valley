using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "ItemSource", menuName = "SO/ItemSource")]
    public class ItemSourceSO : ScriptableObject
    {
        public float FollowSpeed;
        public float FollowDistance;
        public float FollowTime;
        public float PushSpeed;
        public float GravityScale;
        public float PushDistance;
    }
}
