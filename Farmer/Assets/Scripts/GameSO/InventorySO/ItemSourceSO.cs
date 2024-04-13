using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "ItemSource", menuName = "SO/ItemSource")]
    public class ItemSourceSO : ScriptableObject
    {
        [Header("Скорость следования за игроком")]
        public float FollowSpeed;
        [Header("Дистанция, на которой предмет начинает следовать за игроком")]
        public float FollowDistance;
        [Header("Скорость, с которой вылетает ресурс из инвентаря")]
        public float PushSpeed;
        public float GravityScale;
        [Header("Дистанция, на которую перемещается ресурс, который выпал с игрока")]
        public float PushDistance;
        [Header("Дистанция, от игрока к ресурсу, при которой ресурс подбирается")]
        public float PickupDistance;
        [Header("Дистанция между конечной точкой броска и текущей, чтобы перейти в состояние <На земле>")]
        public float DistanceToChangeGroundState;
    }
}
