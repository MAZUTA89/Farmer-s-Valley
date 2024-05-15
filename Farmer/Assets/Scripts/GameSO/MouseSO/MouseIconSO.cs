using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Scripts;
using Scripts.MouseHandle;

namespace Scripts.SO.MouseSO
{
    [CreateAssetMenu(fileName ="MouseIcon", menuName ="SO/Mouse/MouseIcon")]
    public class MouseIconSO : ScriptableObject
    {
        public CursorType CursorType;
        public Texture2D CursorIcon;
    }
}
