using Scripts.SO.MouseSO;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.MouseHandle
{
    [CreateAssetMenu(fileName = "MouseIconsData", menuName ="SO/Mouse/MouseIconsDatabase")]
    public class MouseIconDatabase : ScriptableObject
    {
        public List<MouseIconSO> Data;

        public Dictionary<CursorType, MouseIconSO> _icons;

        public void Init()
        {
            _icons = new Dictionary<CursorType, MouseIconSO>();

            if (Data.Count > 0)
            {
                foreach (MouseIconSO icon in Data)
                {
                    if (_icons.ContainsKey(icon.CursorType) == false)
                    {
                        _icons.Add(icon.CursorType, icon);
                    }
                }
            }
        }

        public Texture2D GetIconByType(CursorType type)
        {
            if (_icons.ContainsKey(type))
            {
                return _icons[type].CursorIcon;
            }
            else
            {
                return default;
            }
        }
    }
}
