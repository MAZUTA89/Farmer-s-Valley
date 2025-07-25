﻿using UnityEngine;

namespace Scripts.SO.Player
{
    [CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
    public class PlayerSO : ScriptableObject
    {
        public float Speed;
        public int StartMoney;
    }
}
