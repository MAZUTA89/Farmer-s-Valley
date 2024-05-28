using UnityEngine;
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
