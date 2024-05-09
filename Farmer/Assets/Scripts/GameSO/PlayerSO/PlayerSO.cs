using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.SO.Player
{
    [CreateAssetMenu(fileName = "PlayerSO", menuName = "SO/PlayerSO")]
    public class PlayerSO : ScriptableObject
    {
        public float Speed;
        public int StartMoney;
    }
}
