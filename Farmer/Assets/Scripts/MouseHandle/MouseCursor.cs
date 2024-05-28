using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.MouseHandle
{
    public enum CursorType
    {
        Default,
        Drag,
        Menu
    }
    public class MouseCursor
    {
        MouseIconDatabase _mouseIconDatabase;
        CursorType _lastType;
        CursorType _currentType;
        public MouseCursor(MouseIconDatabase mouseIconDatabase)
        {
              _mouseIconDatabase = mouseIconDatabase;
            _mouseIconDatabase.Init();
        }

        public void ChangeCursor(CursorType cursorType)
        {
            _lastType = _currentType;
            Texture2D texture2D = _mouseIconDatabase.GetIconByType(cursorType);
            Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.Auto);
            _currentType = cursorType;
        }

        public async void ImitateClick()
        {
            ChangeCursor(CursorType.Drag);
            await Task.Delay(100);
            ToDefault();
        }
        void ToDefault()
        {
            ChangeCursor(_lastType);
        }
    }
}
